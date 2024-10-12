using System.Collections.Generic;
using Ships;
using UnityEngine;

namespace Managers
{
    public class HealthManager : MonoBehaviour
    {
        #region References
        
        private PlayerShip _playerShip;
        
        #endregion
        
        #region Getters and Setters

        public Ship Ship { get; set; }

        #endregion

        #region Methods
        
        // Start is called before the first frame update
        private void Start()
        {
            _playerShip = FindObjectOfType<PlayerShip>();
        }

        // Update is called once per frame
        private void Update()
        {
        
        }
        
        /// <summary>
        /// Reduces ship health by the given damage.
        /// If health is less than or equal to 0, calls the ShipDeath method.
        /// </summary>
        /// <param name="damage"></param>
        internal void TakeDamage(int damage)
        {
            if (Ship.TemporaryHealth > 0)
            {
                if (Ship.TemporaryHealth > damage)
                    Ship.TemporaryHealth -= damage;
                else
                {
                    int remainingDamage = damage - Ship.TemporaryHealth;
                    Ship.TemporaryHealth = 0;
                    Ship.Health -= remainingDamage;
                }
            }
            else
            {
                Ship.Health -= damage;
            }

            // If health is less than or equal to 0, call the ShipDeath method
            ChecksIfShipIsDead();
        }

        /// <summary>
        /// Reduces ship health by the given damage. Ignores temporary health.
        /// If health is less than or equal to 0, calls the ShipDeath method.
        /// </summary>
        /// <param name="damage"></param>
        internal void TakePiercingDamage(int damage)
        {
            Ship.Health -= damage;
            
            // If health is less than or equal to 0, call the ShipDeath method
            ChecksIfShipIsDead();
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

        /// <summary>
        /// Checks if the ship's health is less than or equal to 0.
        /// </summary>
        private void ChecksIfShipIsDead()
        {
            if (Ship.Health > 0) return;
            switch (Ship)
            {
                case BossShip bossShip:
                    bossShip.ShipDeath();
                    break;
                case EnemyShip enemyShip:
                    enemyShip.ShipDeath();
                    foreach (KeyValuePair<Resource, int> loot in enemyShip.Loot)
                    {
                        _playerShip.AddResourceToInventory(loot.Key, loot.Value);
                    }
                    break;
                case PlayerShip playerShip:
                    playerShip.ShipDeath();
                    break;
            }
        }
        
        #endregion
    }
}
