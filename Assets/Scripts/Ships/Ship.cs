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

        [field: Header("Ship References")]
        public int Health { get; set; }

        public int MaxHealth { get; set; }

        public Sprite Sprite { get; set; }

        #endregion

        #region Methods
        
        // Start is called before the first frame update
        private void Start()
        {
            HealthManager = FindObjectOfType<HealthManager>();
            HealthManager.Ship = this;
        }

        // Update is called once per frame
        private void Update()
        {
        
        }        

        #endregion
    }
}
