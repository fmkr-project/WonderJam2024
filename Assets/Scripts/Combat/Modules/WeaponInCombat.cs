using System;
using Managers;
using Modules;
using Ships;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class WeaponInCombat : ModuleInCombat
{
    public bool isTicked = false;
    public int atk;
    public WeaponType weaponType;
    public GameObject enemy;
    private EnemyShip _ship;
    private EnemyInCombat _enemyInCombat;

    

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