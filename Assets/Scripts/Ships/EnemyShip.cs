using System.Collections.Generic;
using System.Linq;
using Modules;
using ScriptableObjects.Scripts;
using UnityEngine;

namespace Ships
{
    public class EnemyShip : Ship
    {
        #region References
        
        [SerializeField] private EnemyShipLootTableScriptableObject enemyShipLootTableScriptableObject; // Must be assigned in the inspector.
        [SerializeField] private EnemyShipScriptableObject enemyShipScriptableObject; // Must be assigned in the inspector.

        #endregion
        
        #region Getters and Setters
        
        public Dictionary<Resource, int> Loot { get; } = new();

        #endregion
        
        #region Methods
        
        // Start is called before the first frame update
        private void Awake()
        {
            ShipInitialization();
            EnemyShipInitialization();
            GenerateLoot();
        }

        // Update is called once per frame
        private void Update()
        {
            
        }

        /// <summary>
        /// Triggers the ship's death.
        /// Rolls on the LootTable and drops the loot.
        /// This method is called when the ship's health is less than or equal to 0.
        /// </summary>
        internal void ShipDeath()
        {
            Destroy(gameObject);
        }
        
        /// <summary>
        /// Generates the loot that the enemy will drop when it dies.
        /// Updates the Loot property with the generated loot.
        /// </summary>
        protected void GenerateLoot()
        {
            // Generate money drop
            int moneyDrop = Random.Range(enemyShipLootTableScriptableObject.minMoneyDrop, enemyShipLootTableScriptableObject.maxMoneyDrop);
            Loot.Add(Resource.Money, moneyDrop);
            
            // Generate scrap drop
            int scrapDrop = Random.Range(0, 100);
            if (scrapDrop < enemyShipLootTableScriptableObject.scrapDropProbability)
            {
                Loot.Add(Resource.Scrap, Random.Range(enemyShipLootTableScriptableObject.minScrapDrop, enemyShipLootTableScriptableObject.maxScrapDrop));
            }
            
            // Generate crew drop
            int crewDrop = Random.Range(0, 100);
            if (crewDrop < enemyShipLootTableScriptableObject.crewDropProbability)
            {
                Loot.Add(Resource.Crew, 1);
            }
            
            // Generate ether drop
            int etherDrop = Random.Range(0, 100);
            if (etherDrop < enemyShipLootTableScriptableObject.etherDropProbability)
            {
                Loot.Add(Resource.Ether, 1);
            }
        }
        
        /// <summary>
        /// Checks if the gameObject using this script has the EnemyShipLootTableScriptableObject and the EnemyShipScriptableObject assigned.
        /// </summary>
        protected void OnValidate()
        {
            if (enemyShipLootTableScriptableObject == null)
            {
                Debug.LogError($"{nameof(enemyShipLootTableScriptableObject)} is not assigned in {nameof(EnemyShip)} script attached to {gameObject.name}");
            }
            
            if (enemyShipScriptableObject == null)
            {
                Debug.LogError($"{nameof(enemyShipScriptableObject)} is not assigned in {nameof(EnemyShip)} script attached to {gameObject.name}");
            }
        }
        
        /// <summary>
        /// Initializes the ship with the values from the EnemyShipScriptableObject.
        /// </summary>
        protected void EnemyShipInitialization()
        {
            if (enemyShipScriptableObject == null)
            {
                Debug.LogError("EnemyShipScriptableObject is not assigned.");
                return;
            }

            if (moduleManager == null)
            {
                Debug.LogError("ModuleManager is not initialized.");
                return;
            }
            
            Health = enemyShipScriptableObject.health;
            MaxHealth = enemyShipScriptableObject.maxHealth;
            TemporaryHealth = enemyShipScriptableObject.temporaryHealth;
            Sprite = enemyShipScriptableObject.sprite;
            
/*            foreach (Shield shield in enemyShipScriptableObject.shieldModules.Select(shieldModule => new Shield(
                         shieldModule.moduleName, 
                         shieldModule.sprite,
                         shieldModule.requiredCrew,
                         shieldModule.price,
                         shieldModule.shieldType,
                         shieldModule.shieldHealth
                     )))
            {
                moduleManager.AddModuleToShip(shield);
            }
            */
            foreach (Weapon weapon in enemyShipScriptableObject.weaponModules.Select(weaponModule => new Weapon(
                         weaponModule.moduleName,
                         weaponModule.sprite,
                         weaponModule.requiredCrew,
                         weaponModule.price,
                         weaponModule.weaponType,
                         weaponModule.weaponDamage
                     )))
            {
                moduleManager.AddModuleToShip(weapon);
            }
        }

        #endregion
    }
}
