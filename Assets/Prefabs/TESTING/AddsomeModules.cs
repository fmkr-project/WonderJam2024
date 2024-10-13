using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Modules;
using Ships;
using UnityEngine;

public class AddsomeModules : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        add();
    }

    public void add()
    {
        for (int i = 0; i < 5; i++)
        {
            Weapon w = new Weapon();
            w.WeaponDamage = i;
            w.RequiredCrew = i;
            FindObjectOfType<PlayerShip>().moduleManager.AddModuleToShip(w);
        } 
        for (int i = 0; i < 5; i++)
        {
            Shield w = new Shield();
            w.ShieldHealth = i;
            w.RequiredCrew = i;
            FindObjectOfType<PlayerShip>().moduleManager.AddModuleToShip(w);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
