using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Ships
{
    public class EnemyShip : Ship
    {
        #region References
        
        [SerializeField] private EnemyShipLootTableScriptableObject enemyShipLootTableScriptableObject; // Must be assigned in the inspector.

        #endregion
        
        #region Getters and Setters
        
        public Dictionary<Resource, int> Loot { get; set; }

        #endregion
        
        #region Methods
        
        // Start is called before the first frame update
        private void Start()
        {
            Loot = new Dictionary<Resource, int>();
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
            
        }
        
        /// <summary>
        /// Generates the loot that the enemy will drop when it dies.
        /// Updates the Loot property with the generated loot.
        /// </summary>
        /// <returns></returns>
        private Dictionary<Resource, int> GenerateLoot()
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
            
            return Loot;
        }
        
        /// <summary>
        /// Checks if the gameObject using this script has the EnemyShipLootTableScriptableObject assigned.
        /// </summary>
        private void OnValidate()
        {
            if (enemyShipLootTableScriptableObject == null)
            {
                Debug.LogError($"{nameof(enemyShipLootTableScriptableObject)} is not assigned in {nameof(EnemyShip)} script attached to {gameObject.name}");
            }
        }

        #endregion
    }
}
