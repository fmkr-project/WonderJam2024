using System;
using System.Collections;
using System.Collections.Generic;
using Modules;
using Ships;
using UnityEngine;
using UnityEngine.Serialization;

public class ShieldInCombat : ModuleInCombat
{
    public int shieldHealth;
    public ShieldType shieldType;
    private PlayerShip _playerShip;

    private void Awake()
    {
        _playerShip = FindObjectOfType<PlayerShip>();
    }

    public override void Tick()
    {
        if(!isUsed) return;
        
        switch (shieldType)
        {
            case ShieldType.Shield:
                _playerShip.healthManager.Shield(shieldHealth);
                break;
            
            case ShieldType.RegenerativeShield:
                _playerShip.healthManager.Heal(shieldHealth);
                break;
            
            case ShieldType.PlasmaShield:
                _playerShip.HasPlasmaShield = true;
                _playerShip.healthManager.Shield(shieldHealth);
                break;
            
            case ShieldType.PsionicShield:
                _playerShip.HasPsionicShield = true;
                _playerShip.healthManager.Shield(shieldHealth);
                break;
            
            default:
                Debug.Log("Shield type not found");
                throw new ArgumentOutOfRangeException();
        }
    }
}
