using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }
    
    [Header("Settings")]
    public GameObject spaceShip; // Prefab for the spaceShip
    public Transform enemyParent; // Parent for enemies
    public GameObject weaponSelectionMenu; // Weapon selection menu

    public List<GameObject> enemies;
    private List<GameObject> enemiesInstantiated = new List<GameObject>();

    [SerializeField] private List<GameObject> temporaryModules;

    private List<ModuleInCombat> _modules = new List<ModuleInCombat>(); // Modules retrieved from the ship
    public List<Transform> modulesParents; // 1 : weapon, 2 : shhield, 3: other
    public List<GameObject> modulesPrefabs; // same order as in modulesParents
    [SerializeField] private int _maxPeople; // Peoples retrieves from the ship
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // Instantiate enemies at startup
        StartCoroutine(SpawnEnemies());
        SpawnModules();
    }

    private void SpawnModules()
    {
        //  TODO : changer l'implementation pour mettre les vrais armes et tout
        if (1 == 0)
        {
            List<ModuleInCombat> mmmm = new List<ModuleInCombat>(); //new List<ModuleInCombat>(ship.GetModules());
            foreach (var mod in mmmm)
            {   
                // if mod is weapon
                GameObject weapon = Instantiate(modulesPrefabs[0], modulesParents[0]);
                _modules.Add(weapon.GetComponent<ModuleInCombat>());
                // if mod is shield
                GameObject shield = Instantiate(modulesPrefabs[1], modulesParents[1]);
                _modules.Add(shield.GetComponent<ModuleInCombat>());
                // if mod is other
                GameObject other = Instantiate(modulesPrefabs[2], modulesParents[2]);
                _modules.Add(other.GetComponent<ModuleInCombat>());
                //fix all the values the module in combat should have !
            }
        }
        else
        {
            foreach (var gm in temporaryModules)
            {
                print("sheeeeeh");
                var mod = Instantiate(gm, modulesParents[0]);
                mod.SetActive(true);
                _modules.Add(mod.GetComponent<ModuleInCombat>());
            }
        }
    }
    private IEnumerator SpawnEnemies()
    {
        foreach(var enemyPrefab in enemies)
        {
            print("sheh");
            GameObject enemy = Instantiate(enemyPrefab, enemyParent);
            enemiesInstantiated.Add(enemy);
            yield return new WaitForSeconds(0.5f); // Delay between each enemy
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
        // Call the player's turn function
        Debug.Log("It's the player's turn!");
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
        //TODO : check if player is alive
    }

    /*public void InitializeModules(Ship ship)
    {
        // Retrieve the modules from the ship's information
        _modules = new List<ModuleInCombat>(ship.GetModules());
    }*/
}
