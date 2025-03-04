using System.Collections;
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
                    Ship.HasPlasmaShield = false;
                    Ship.HasPsionicShield = false;
                    Ship.Health -= remainingDamage;
                }
            }
            else
            {
                Ship.Health -= damage;
            }

            StartCoroutine(FlashDamageEffect(Ship));

            // If health is less than or equal to 0, call the ShipDeath method
            ChecksIfShipIsDead();
        }
        
        public IEnumerator FlashDamageEffect(Ship ship)
        {
            var _spriteRenderer = ship.GetComponent<SpriteRenderer>();
            var originalColor = _spriteRenderer.color;
            var originalPosition = ship.transform.localPosition;
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.color= Color.white;
            float elapsed = 0f;

            while (elapsed < 0.5f)
            {
                // Générer un déplacement aléatoire pour le vacillement
                float offsetX = Random.Range(-0.1f, 0.1f);
                float offsetY = Random.Range(-0.1f, 0.1f);
                float offsetZ = Random.Range(-0.1f, 0.1f);

                // Appliquer la position vacillante
                ship.transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, offsetZ);
                elapsed += Time.deltaTime;

                yield return null; // Attendre la prochaine frame
            }

            ship.transform.localPosition = originalPosition; // Restaurer la position initiale
        }

        /// <summary>
        /// Reduces ship temporary health by the given damage.
        /// </summary>
        /// <param name="damage"></param>
        internal void TakeShieldDamage(int damage)
        {
            if (Ship.TemporaryHealth > damage)
                Ship.TemporaryHealth -= damage;
            else
            {
                Ship.TemporaryHealth = 0;
                Ship.HasPlasmaShield = false;
                Ship.HasPsionicShield = false;
            }
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
        /// Cannot heal more than the ship's max health.
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
        /// Shields the ship by the given shield amount.
        /// Cannot shield more than the ship's max health.
        /// </summary>
        /// <param name="shield"></param>
        internal void Shield(int shield)
        {
            Ship.TemporaryHealth += shield;
            if (Ship.TemporaryHealth > Ship.MaxHealth)
            {
                Ship.TemporaryHealth = Ship.MaxHealth;
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
                    foreach (KeyValuePair<Resource, int> loot in enemyShip.Loot)
                    {
                        print(loot);
                        _playerShip.AddResourceToInventory(loot.Key, loot.Value);
                    }
                    enemyShip.ShipDeath();
                    break;
                case PlayerShip playerShip:
                    //playerShip.ShipDeath();
                    break;
            }
        }
        
        #endregion
    }
}
