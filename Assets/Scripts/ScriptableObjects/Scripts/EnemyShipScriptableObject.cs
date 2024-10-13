using System.Collections.Generic;
using Modules;
using UnityEngine;

namespace ScriptableObjects.Scripts
{
    [CreateAssetMenu(fileName = "EnemyShip", menuName = "ScriptableObjects/EnemyShip", order = 1)]
    public class EnemyShipScriptableObject : ScriptableObject
    {
        public int health;
        public int maxHealth;
        public int temporaryHealth;
        public Sprite sprite;
        public List<ShieldModuleScriptableObject> shieldModules;
        public List<WeaponModuleScriptableObject> weaponModules;
    }
}
