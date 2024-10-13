using System.Collections.Generic;
using Ships;
using UnityEngine;
using Upgrades;

namespace Managers
{
    public static class GameManager
    {
        public static PlayerShip CurrentPlayerShip { get; set; }
        public static int CurrentRun { get; set; }

        public static List<RebirthUpgrade> RebirthUpgrades = new();

        public static float progress;
        public static string map = "Map/MAP 2";
        public static Vector3 currentShipPosition;

        public static void NewGame()
        {
            CurrentRun = 1;
            var DELETE_ME = 8;
        }
    }
}
