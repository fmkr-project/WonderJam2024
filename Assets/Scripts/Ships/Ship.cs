using System;
using System.Collections.Generic;
using Managers;
using Modules;
using UnityEngine;

namespace Ships
{
    public class Ship : MonoBehaviour
    {
        #region References

        protected HealthManager HealthManager;
        protected ModuleManager ModuleManager;

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
            HealthManager = FindObjectOfType<HealthManager>();
            HealthManager.Ship = this;
            ModuleManager = FindObjectOfType<ModuleManager>();
            ModuleManager.Ship = this;
        }

        // Update is called once per frame
        private void Update()
        {
        
        }        

        #endregion
    }
}
