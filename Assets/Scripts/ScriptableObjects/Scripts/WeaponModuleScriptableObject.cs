using Modules;
using UnityEngine;

namespace ScriptableObjects.Scripts
{
    [CreateAssetMenu(fileName = "WeaponModule", menuName = "ScriptableObjects/WeaponModule", order = 1)]
    public class WeaponModuleScriptableObject : ScriptableObject
    {
        public string moduleName;
        public Sprite sprite;
        public ResourceAmount price;
        public int requiredCrew;
        public int weaponDamage;
        public WeaponType weaponType;
    }
}