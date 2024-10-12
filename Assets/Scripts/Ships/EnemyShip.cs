using UnityEngine;

namespace Ships
{
    public class EnemyShip : Ship
    {
        // Start is called before the first frame update
        private void Start()
        {
        
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
    }
}
