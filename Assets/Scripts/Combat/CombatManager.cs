using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Modules;
using Ships;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using Upgrades;
using Random = UnityEngine.Random;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }
    
    [Header("Settings")]
    public Transform spaceShipPosition; // Prefab for the spaceShip
    public Transform enemyParent; // Parent for enemies
    public GameObject weaponSelectionMenu; // Weapon selection menu

    public List<GameObject> enemies;
    public List<GameObject> enemiesInstantiated = new List<GameObject>();

    [SerializeField] private List<GameObject> temporaryModules;

    private List<ModuleInCombat> _modules = new List<ModuleInCombat>(); // Modules retrieved from the ship
    public List<Transform> modulesParents; // 1 : weapon, 2 : shhield, 3: other
    public List<GameObject> modulesPrefabs; // same order as in modulesParents
    [SerializeField] private int _maxPeople; // Peoples retrieves from the ship
    private PlayerShip _playerShip;
    private bool win;

    private void Start()
    {
        _playerShip = FindObjectOfType<PlayerShip>();
        _playerShip.transform.position = spaceShipPosition.position;
        if (!FindObjectOfType<Fantomes>().IsUnityNull()) _playerShip.transform.localRotation = quaternion.Euler(0,0,math.PI/2);
        _playerShip.transform.localScale = Vector3.one*1.2f;
        _maxPeople = _playerShip.Inventory[Resource.Crew];
        StartCoroutine(SpawnEnemies());
        SpawnModules();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void SpawnModules()
    {
            foreach (var mod in _playerShip.Modules)
            {
                switch (mod)
                {
                    case Weapon mod1:
                        GameObject weapon = Instantiate(modulesPrefabs[0], modulesParents[0]);
                        weapon.SetActive(true);
                        weapon.GetComponent<CreateWeaponCombat>().Create(mod1);
                        _modules.Add(weapon.GetComponent<ModuleInCombat>());
                        break;
                    case Shield mod1:
                        GameObject shield = Instantiate(modulesPrefabs[1], modulesParents[1]);
                        shield.SetActive(true);
                        shield.GetComponent<CreateShieldCombat>().Create(mod1);
                        _modules.Add(shield.GetComponent<ModuleInCombat>());
                        break;
                }
                
            }
    }

    private IEnumerator SpawnEnemies()
    {
        if (!(enemiesInstantiated.Count >= 1))
        {
            if (FindObjectOfType<Fantomes>().IsUnityNull())
            {
                
                int numberenemies = Random.Range(1, 1 + _playerShip.Modules.Count / 4);
                for (int i = 0; i < numberenemies; i++)
                {
                    int randomIndex = Random.Range(0, enemies.Count);
                    GameObject enemy = Instantiate(enemies[randomIndex], enemyParent);
                    enemy.transform.localPosition += Vector3.down*(i*2);
                    //enemy.transform.localRotation = Quaternion.Euler(0,0,90);
                    enemiesInstantiated.Add(enemy);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            else
            {
                GameObject enemy = Instantiate(enemies[0], enemyParent);
                //enemy.transform.localRotation = Quaternion.Euler(0,0,90);
                enemiesInstantiated.Add(enemy);
                yield return new WaitForSeconds(0.5f);
            }
        }
        SelectModule.Instance.UpdateEnemies();
        StartPlayerTurn(); // Start the player's turn
    }

    private void StartPlayerTurn()
    {
        if (_playerShip.Health < 0 || win) return;
        double buffer = 1;
        foreach (RebirthUpgrade upgrade in GameManager.RebirthUpgrades)
        {
            if (upgrade.Name == "ShieldRegen")
            {
                buffer -= 0.1;
            }
        }

        _playerShip.TemporaryHealth -= (int)(buffer * _playerShip.TemporaryHealth);
        foreach (var mod in _modules)
        {
            mod.Reset();
        }

        SelectModule.Instance.ResetPeople(_maxPeople);
        weaponSelectionMenu.SetActive(true); // Activate the selection menu
    }

    public void EndTurn()
    {
        weaponSelectionMenu.SetActive(false); // Deactivate the menu
        foreach (var module in _modules)
        {
            module.Tick(); // TODO: Maybe add time between each action?
            
        }

        StartCoroutine(StartEnemyTurn());
    }

    private IEnumerator StartEnemyTurn()
    {
        foreach (var enemyGameobject in enemiesInstantiated)
        {
            if(enemyGameobject.IsUnityNull()) continue;
            if(enemyGameobject.TryGetComponent(out EnemyInCombat enemy))
                enemy.TakeAction(); // Enemy's action
            yield return new WaitForSeconds(0.5f);
        }

        // Check the game state (win/lose)
        CheckGameOver();
        
        StartPlayerTurn(); // Return to the player's turn
    }

    private void CheckGameOver()
    {
        if (_playerShip.Health <0) 
        {
            //TODO : you loose;
            GameManager.currentShipPosition = Vector3.zero;
            GameManager.progress = 0;
            if (GameManager.CurrentRun > 0)
            {
                _playerShip.ShipDeath();
                SceneManager.LoadScene("DeathRebirth");
            }
            else
            {
                _playerShip.GetComponent<ArcMovement>().enabled = true;
                StartCoroutine(WaitForThreeSecond());
            }
            return;
        }

        if (!checkEnemies())
        {
            win = true;
            //TODO : You win;
            var fant = FindObjectOfType<Fantomes>();
            if(!fant.IsUnityNull())
            {
                fant.End();
            }
            else
            {
                if (GameManager.progress == 0)
                    SceneManager.LoadScene("SceneCombatFantome");
                else 
                    SceneManager.LoadScene("Map/MAP 2");
                
            }
        }
    }
    
    private IEnumerator WaitForThreeSecond()
    {
        // Attend 1 seconde
        yield return new WaitForSeconds(3f);
        _playerShip.ShipDeath();
        SceneManager.LoadScene("Scenes/Tuto");
        
        Debug.Log("1 seconde s'est écoulée !");
    }

    private bool checkEnemies()
    {
        foreach (var enemy in enemiesInstantiated)
        {
            if (!enemy.IsUnityNull()) return true;
        }

        return false;
    }

    /*public void InitializeModules(Ship ship)
    {
        // Retrieve the modules from the ship's information
        _modules = new List<ModuleInCombat>(ship.GetModules());
    }*/
}
