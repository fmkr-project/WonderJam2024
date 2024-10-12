using UnityEngine;

namespace Ships
{
    public class BossShip : EnemyShip
    {
        // Start is called before the first frame update
        private void Start()
        {
            ShipInitialization();
            EnemyShipInitialization();
            //TODO : Generate Boss Loot
        }

        // Update is called once per frame
        private void Update()
        {

        }
        
        /// <summary>
        /// Triggers the ship's death.
        /// Rolls on the LootTable and drops the loot.
        /// Also generates next level or triggers the end of the game.
        /// This method is called when the ship's health is less than or equal to 0.
        /// </summary>
        internal new void ShipDeath()
        {
            Destroy(gameObject);
        }
    }
}
