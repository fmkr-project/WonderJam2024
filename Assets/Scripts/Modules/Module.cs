using UnityEngine;

namespace Modules
{
    public class Module : MonoBehaviour
    {
        #region References
        
        public string Name;
        public Sprite Sprite;
        public int RequiredCrew;
        public ModuleClass ModuleClass;
        
        #endregion
        
        #region Getters and Setters
        
        public ResourceAmount Price { get; set; }

        #endregion
        
        #region Methods
        
        #endregion
    }
}
