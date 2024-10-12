using System.Collections.Generic;
using UnityEngine;

namespace Ships
{
    public class PlayerShip : Ship
    {
        #region References

        private Dictionary<Resource, int> _inventory;

        #endregion
        
        #region Getters and Setters
        
        #endregion
        
        #region Methods
        
        // Start is called before the first frame update
        private void Start()
        {
            _inventory = new Dictionary<Resource, int>();
        }

        // Update is called once per frame
        private void Update()
        {
        
        }
        
        /// <summary>
        /// Triggers the ship's death.
        /// GameOver menu is displayed ???
        /// This method is called when the ship's health is less than or equal to 0.
        /// </summary>
        internal void ShipDeath()
        {
            Destroy(gameObject);
        }
        
        /// <summary>
        /// Adds the given amount of the given resource to the player's inventory.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="amount"></param>
        private void AddItemToInventory(Resource resource, int amount)
        {
            if (!_inventory.TryAdd(resource, amount))
            {
                _inventory[resource] += amount;
            }
        }
        
        /// <summary>
        /// Removes the given amount of the given resource from the player's inventory.
        /// Does nothing if the player does not have enough of the given resource.
        /// Should be called after testing if the player has enough of the given resource with the ChecksIfPlayerHasEnoughOfTheGivenResource method.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="amount"></param>
        private void RemoveItemFromInventory(Resource resource, int amount)
        {
            if (!_inventory.ContainsKey(resource)) return;
            _inventory[resource] -= amount;
            if (_inventory[resource] <= 0)
            {
                _inventory.Remove(resource);
            }
        }
        
        /// <summary>
        /// Checks if the player has enough of the given resource.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private bool ChecksIfPlayerHasEnoughOfTheGivenResource(Resource resource, int amount)
        {
            return _inventory.ContainsKey(resource) && _inventory[resource] >= amount;
        }
        
        #endregion
    }
}
