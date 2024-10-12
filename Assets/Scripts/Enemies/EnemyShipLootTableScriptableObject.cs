using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "EnemyShipLootTable", menuName = "ScriptableObjects/EnemyShipLootTable", order = 1)]
    public class EnemyShipLootTableScriptableObject : ScriptableObject
    {
        public int minMoneyDrop;
        public int maxMoneyDrop;
        public int scrapDropProbability;
        public int minScrapDrop;
        public int maxScrapDrop;
        public int crewDropProbability;
        public int etherDropProbability;
    }
}
