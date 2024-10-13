using UnityEngine;

namespace ScriptableObjects.Scripts
{
    [CreateAssetMenu(fileName = "ShieldModule", menuName = "ScriptableObjects/ShieldModule", order = 1)]
    public class ShieldModuleScriptableObject : ScriptableObject
    {
        public string moduleName;
        public Sprite sprite;
        public ResourceAmount price;
        public int requiredCrew;
        public int shieldHealth;
    }
}