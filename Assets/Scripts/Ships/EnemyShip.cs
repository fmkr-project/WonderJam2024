using System.Collections.Generic;
using UnityEngine;

namespace Ships
{
    public class EnemyShip : Ship
    {
        #region References
        
        [Header("Loots")]
        [SerializeField] private int minMoneyDrop;
        [SerializeField] private int maxMoneyDrop;
        [SerializeField] private int crewDropProbability;
        [SerializeField] private int etherDropProbability;

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
            GenerateLoot();
            Destroy(gameObject);
        }
        
        /// <summary>
        /// Generates the loot that the enemy will drop when it dies.
        /// Updates the Loot property with the generated loot.
        /// </summary>
        /// <returns></returns>
        private Dictionary<Resource, int> GenerateLoot()
        {
            // Generate money drop
            int moneyDrop = Random.Range(minMoneyDrop, maxMoneyDrop);
            Loot.Add(Resource.Money, moneyDrop);
            
            // Generate crew drop
            int crewDrop = Random.Range(0, 100);
            if (crewDrop < crewDropProbability)
            {
                Loot.Add(Resource.Crew, 1);
            }
            
            // Generate ether drop
            int etherDrop = Random.Range(0, 100);
            if (etherDrop < etherDropProbability)
            {
                Loot.Add(Resource.Ether, 1);
            }
            
            return Loot;
        }

        #endregion
    }
}
