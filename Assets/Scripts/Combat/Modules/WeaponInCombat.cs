using Managers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class WeaponInCombat : ModuleInCombat
{
    public bool isTicked = false;
    public int atk;
    public GameObject enemy;
        
    public override void Tick()
    {
        if(enemy.IsUnityNull()) return;
        enemy.GetComponent<HealthManager>().TakeDamage(atk);
    }

    public override void Reset()
    {
        base.Reset();
        isTicked = false;
        enemy = null;
    }

}