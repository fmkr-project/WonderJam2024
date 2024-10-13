using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Modules;
using Ships;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Start()
    {
        _playerShip = FindObjectOfType<PlayerShip>();
        _playerShip.transform.position = spaceShipPosition.position;
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
            int randomIndex = Random.Range(0, enemiesInstantiated.Count);
            GameObject enemy = Instantiate(enemiesInstantiated[randomIndex], enemyParent);
            enemiesInstantiated.Add(enemy);
            yield return new WaitForSeconds(0.5f);
            
        }
        StartPlayerTurn(); // Start the player's turn
    }

    private void StartPlayerTurn()
    {
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
                SceneManager.LoadScene("Upgrade");
            
            _playerShip.GetComponent<ArcMovement>().enabled = true;
            StartCoroutine(WaitForThreeSecond());
            
        }

        if (!checkEnemies())
        {
            //TODO : You win;
            SceneManager.LoadScene("Map/MAP 2");
        }
    }
    
    private IEnumerator WaitForThreeSecond()
    {
        // Attend 1 seconde
        yield return new WaitForSeconds(3f);
        Destroy(_playerShip.gameObject);
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
