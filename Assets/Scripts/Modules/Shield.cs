using UnityEngine;

namespace Modules
{
    public class Shield : Module
    {
        #region References

        public int ShieldHealth;

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
        public Shield(string moduleName, Sprite sprite, int requiredCrew, ResourceAmount price, int shieldHealth) : base(moduleName, sprite, requiredCrew, price)
        {
            ShieldHealth = shieldHealth;
        }
        
        #endregion
    }
}