using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ships
{
    public class Ship : MonoBehaviour
    {
        #region References

        protected HealthManager HealthManager;
        
        #endregion

        #region Getters and Setters

        [field: Header("Ship References")]
        public int Health { get; set; }

        public int MaxHealth { get; set; }

        public Sprite Sprite { get; set; }

        #endregion
        
        
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
    }
}
