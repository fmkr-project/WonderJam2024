using UnityEngine;

namespace Modules
{
    public class Shield : Module
    {
        #region References

        public int ShieldHealth;
        public ShieldType ShieldType;

        #endregion
        
        #region Constructors 
        
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="sprite"></param>
        /// <param name="requiredCrew"></param>
        /// <param name="price"></param>
        /// <param name="shieldHealth"></param>
        /// <param name="shieldType"></param>
        public Shield(string moduleName, Sprite sprite, int requiredCrew, ResourceAmount price, ShieldType shieldType, int shieldHealth) : base(moduleName, sprite, requiredCrew, price)
        {
            ShieldType = shieldType;
            ShieldHealth = shieldHealth;
        }

        public Shield()
        {
            
        }
        
        #endregion
    }
}