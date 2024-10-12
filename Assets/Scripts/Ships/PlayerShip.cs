using System.Collections.Generic;

namespace Ships
{
    public class PlayerShip : Ship
    {
        #region References

        #endregion
        
        #region Getters and Setters

        private Dictionary<Resource, int> Inventory { get; set; }

        #endregion
        
        #region Methods
        
        // Start is called before the first frame update
        private void Start()
        {
            Inventory = new Dictionary<Resource, int>();
        }

        // Update is called once per frame
        private void Update()
        {

        }
        
        /// <summary>
        /// Triggers the ship's death.
        /// GameOver menu is displayed ????
        /// This method is called when the ship's health is less than or equal to 0.
        /// </summary>
        internal void ShipDeath()
        {
            
        }
        
        /// <summary>
        /// Adds the given amount of the given resource to the player's inventory.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="amount"></param>
        internal void AddResourceToInventory(Resource resource, int amount)
        {
            if (!Inventory.TryAdd(resource, amount))
            {
                Inventory[resource] += amount;
            }
        }
        
        /// <summary>
        /// Removes the given amount of the given resource from the player's inventory.
        /// Does nothing if the player does not have enough of the given resource.
        /// Should be called after testing if the player has enough of the given resource with the ChecksIfPlayerHasEnoughOfTheGivenResource method.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="amount"></param>
        internal void RemoveResourceFromInventory(Resource resource, int amount)
        {
            if (!Inventory.ContainsKey(resource)) return;
            Inventory[resource] -= amount;
            if (Inventory[resource] <= 0)
            {
                Inventory.Remove(resource);
            }
        }
        
        /// <summary>
        /// Checks if the player has enough of the given resource.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        internal bool ChecksIfPlayerHasEnoughOfTheGivenResource(Resource resource, int amount)
        {
            return Inventory.ContainsKey(resource) && Inventory[resource] >= amount;
        }

        internal bool ChecksIfPlayerHasEnoughOfTheGivenResource(ResourceAmount ra)
        {
            return ChecksIfPlayerHasEnoughOfTheGivenResource(ra.Resource, ra.Quantity);
        }
        
        #endregion
    }
}
