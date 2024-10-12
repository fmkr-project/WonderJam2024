using UnityEngine;

namespace Modules
{
    [CreateAssetMenu(fileName = "Module", menuName = "ScriptableObjects/ModuleScriptableObject", order = 1)]
    public class ModuleScriptableObject : ScriptableObject
    {
        public string moduleName;
        public Sprite sprite;
        public int requiredCrew;
        public ModuleClass moduleClass;
        public ResourceAmount price;
    }
}