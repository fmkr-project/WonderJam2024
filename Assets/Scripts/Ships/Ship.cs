using System.Collections.Generic;
using Managers;
using Modules;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ships
{
    public class Ship : MonoBehaviour
    {
        #region References

        public HealthManager healthManager;
        public ModuleManager moduleManager;

        #endregion

        #region Getters and Setters
        
        public int Health { get; set; }

        public int MaxHealth { get; set; }
        
        public int TemporaryHealth { get; set; }

        public Sprite Sprite { get; set; }
        
        public List<Module> Modules { get; set; }

        #endregion

        #region Methods
        
        // Start is called before the first frame update
        private void Start()
        {
            ShipInitialization();
        }

        // Update is called once per frame
        private void Update()
        {
        
        }        
        
        /// <summary>
        /// Initializes the ship with the HealthManager and ModuleManager components.
        /// </summary>
        private void ShipInitialization()
        {
            healthManager = gameObject.AddComponent<HealthManager>();
            healthManager.Ship = this;
            
            moduleManager = gameObject.AddComponent<ModuleManager>();
            moduleManager.Ship = this;
        }

        #endregion
    }
}
