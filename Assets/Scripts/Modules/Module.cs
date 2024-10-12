using UnityEngine;

namespace Modules
{
    public class Module
    {
        #region References
        
        public string ModuleName;
        public Sprite Sprite;
        public int RequiredCrew;
        public ModuleClass ModuleClass;
        
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
        /// <param name="moduleClass"></param>
        /// <param name="price"></param>
        public Module(string moduleName, Sprite sprite, int requiredCrew, ModuleClass moduleClass, ResourceAmount price)
        {
            ModuleName = moduleName;
            Sprite = sprite;
            RequiredCrew = requiredCrew;
            ModuleClass = moduleClass;
            Price = price;
        }
        
        #endregion
        
        #region Methods
        
        #endregion
    }
}
