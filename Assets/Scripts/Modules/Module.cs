using UnityEngine;

namespace Modules
{
    public class Module
    {
        #region References
        
        public string ModuleName;
        public Sprite Sprite;
        public int RequiredCrew;
        
        #endregion
        
        #region Getters and Setters
        
        public ResourceAmount Price { get; set; }

        #endregion
        
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected Module()
        {
            
        }
        
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="sprite"></param>
        /// <param name="requiredCrew"></param>
        /// <param name="price"></param>
        protected Module(string moduleName, Sprite sprite, int requiredCrew, ResourceAmount price)
        {
            ModuleName = moduleName;
            Sprite = sprite;
            RequiredCrew = requiredCrew;
            Price = price;
        }
        
        #endregion
        
        #region Methods
        
        #endregion
    }
}
