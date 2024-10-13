using System.Collections;
using System.Collections.Generic;
using Ships;
using UnityEngine;

public class ShieldInCombat : ModuleInCombat
{
    public int shieldIC;
    public override void Tick()
    {
        FindObjectOfType<PlayerShip>().healthManager.Shield(shieldIC);
    }
}
