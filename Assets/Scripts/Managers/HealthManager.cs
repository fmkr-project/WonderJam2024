using UnityEngine;

namespace Managers
{
    public class HealthManager : MonoBehaviour
    {
        #region Getters and Setters

        public Ships.Ship Ship { get; set; }

        #endregion

        #region Methods
        
        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
        
        }
        
        /// <summary>
        /// Reduces ship health by the given damage. If health is less than or equal to 0, calls the ShipDeath method.
        /// </summary>
        /// <param name="damage"></param>
        internal void TakeDamage(int damage)
        {
            Ship.Health -= damage;
            if (Ship.Health > 0) return;
            switch (Ship)
            {
                case Ships.BossShip bossShip:
                    bossShip.ShipDeath();
                    break;
                case Ships.EnemyShip enemyShip:
                    enemyShip.ShipDeath();
                    break;
                case Ships.PlayerShip playerShip:
                    playerShip.ShipDeath();
                    break;
            }
        }
        
        /// <summary>
        /// Heals the ship by the given heal amount.
        /// </summary>
        /// <param name="heal"></param>
        internal void Heal(int heal)
        {
            Ship.Health += heal;
            if (Ship.Health > Ship.MaxHealth)
            {
                Ship.Health = Ship.MaxHealth;
            }
        }
        
        #endregion
    }
}
