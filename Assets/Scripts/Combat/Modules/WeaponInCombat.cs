using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponInCombat : ModuleInCombat
{
    public bool isTicked = false;

    public GameObject enemy;
        
    public override void Tick()
    {
        //TODO do the thing;
    }

    public override void Reset()
    {
        base.Reset();
        isTicked = false;
        enemy = null;
    }

}