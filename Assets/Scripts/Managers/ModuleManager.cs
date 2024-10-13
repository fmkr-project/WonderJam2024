using System.Collections.Generic;
using Modules;
using Ships;
using UnityEngine;

namespace Managers
{
    public class ModuleManager : MonoBehaviour
    {
        #region References

        public static List<Module> ListOfAllModules;
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
            //Debug.Log("_playerShip initialized: " + (_playerShip != null));
        }

        /// <summary>
        /// Buys the given module and adds it to the player's ship.
        /// Checks if the player has enough resources to buy the module.
        /// </summary>
        /// <param name="module"></param>
        
        internal bool BuyModule(Module module)
        {
            if (Ship is not PlayerShip) return false;
    
            var playerShip = (PlayerShip)Ship;
            if (!playerShip.ChecksIfPlayerHasEnoughOfTheGivenResource(module.Price.Resource, module.Price.Quantity)) 
                return false;
    
            playerShip.RemoveResourceFromInventory(module.Price.Resource, module.Price.Quantity);
            AddModuleToShip(module);
            return true;
        }


        /// <summary>
        /// Sell the given module for half price and removes it from the player's ship.
        /// </summary>
        /// <param name="module"></param>
        internal void SellModule(Module module)
        {
            if (Ship is not PlayerShip) return;
            _playerShip.AddResourceToInventory(module.Price.Resource, module.Price.Quantity / 2);
            RemoveModuleFromShip(module);
        }

        /// <summary>
        /// Adds the given module to the ship.
        /// </summary>
        /// <param name="module"></param>
        internal void AddModuleToShip(Module module)
        {
            if (Ship == null)
            {
                Debug.LogError("Ship is not assigned in ModuleManager.");
                return;
            }

            if (Ship.Modules == null)
            {
                Debug.LogError("Ship.Modules is not initialized.");
                return;
            }

            Ship.Modules.Add(module);
        }

        /// <summary>
        /// Removes the given module from the ship.
        /// </summary>
        /// <param name="module"></param>
        internal void RemoveModuleFromShip(Module module)
        {
            if (Ship == null)
            {
                Debug.LogError("Ship is not assigned in ModuleManager.");
                return;
            }

            if (Ship.Modules == null)
            {
                Debug.LogError("Ship.Modules is not initialized.");
                return;
            }

            if (!Ship.Modules.Contains(module)) return;
            Ship.Modules.Remove(module);
        }

        #endregion
    }
}