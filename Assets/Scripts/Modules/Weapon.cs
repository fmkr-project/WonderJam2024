using UnityEngine;

namespace Modules
{
    public class Weapon : Module
    {
        #region References
        
        public WeaponType WeaponType;
        public int WeaponDamage;
        
        #endregion
        
        #region Constructors
        
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="sprite"></param>
        /// <param name="requiredCrew"></param>
        /// <param name="price"></param>
        /// <param name="weaponType"></param>
        /// <param name="weaponDamage"></param>
        public Weapon(string moduleName, Sprite sprite, int requiredCrew, ResourceAmount price, WeaponType weaponType, int weaponDamage) : base(moduleName, sprite, requiredCrew, price)
        {
            WeaponType = weaponType;
            WeaponDamage = weaponDamage;
        }

        public Weapon()
        {
        }
        
        #endregion
    }
}