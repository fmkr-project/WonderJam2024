using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Modules;
using UnityEngine;

namespace Ships
{
    public class PlayerShip : Ship
    {

        #region References

        #endregion
        
        #region Getters and Setters

        public static PlayerShip Instance { get; private set; }
        internal Dictionary<Resource, int> Inventory { get; } = new();

        #endregion
        
        #region Methods
        
        // Start is called before the first frame update
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance.gameObject);
                Instance = this;
            }
            else
            {
                if(Instance == null)
                    Instance = this;
                else 
                    Destroy(gameObject);
            }
            GameManager.CurrentPlayerShip = this;
            ShipInitialization();
            AddResourceToInventory(Resource.Money, 0);
            AddResourceToInventory(Resource.Crew, 0);
            AddResourceToInventory(Resource.Scrap, 0);
            AddResourceToInventory(Resource.Ether, 0);
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
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
            GameManager.CurrentRun += 1;
            GameManager.ether = Inventory[Resource.Ether];
            gameObject.AddComponent<ShipDataHandler>().SavePlayerShipData();
            Destroy(gameObject);
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