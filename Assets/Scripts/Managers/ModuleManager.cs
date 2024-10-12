using System;
using System.Collections.Generic;
using JetBrains.Annotations;
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
            ListOfAllModules = new List<Module>();
            
            _playerShip = FindObjectOfType<PlayerShip>();
        }

        /// <summary>
        /// Buys the given module and adds it to the player's ship.
        /// Checks if the player has enough resources to buy the module.
        /// </summary>
        /// <param name="module"></param>
        internal void BuyModule(Module module)
        {
            if (Ship is not PlayerShip) return;
            if (!_playerShip.ChecksIfPlayerHasEnoughOfTheGivenResource(module.Price.Resource, module.Price.Quantity)) return;
            _playerShip.RemoveResourceFromInventory(module.Price.Resource, module.Price.Quantity);
            AddModuleToShip(module);
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
        private void AddModuleToShip(Module module)
        {
            Ship.Modules.Add(module);
        }
        
        /// <summary>
        /// Removes the given module from the ship.
        /// </summary>
        /// <param name="module"></param>
        private void RemoveModuleFromShip(Module module)
        {
            if (!Ship.Modules.Contains(module)) return;
            Ship.Modules.Remove(module);
        }
        
        #endregion
    }
}