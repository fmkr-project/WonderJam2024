using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Modules;
using Ships;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class WeaponInCombat : ModuleInCombat
{
    public bool isTicked;
    public int weaponDamage;
    public WeaponType weaponType;
    public GameObject enemy;
    public EnemyShip _enemyShip;
    public EnemyInCombat _enemyInCombat;
    private PlayerShip _playerShip;
    private List<GameObject> _enemiesInstantiated;

    private void Awake()
    {
        _playerShip = FindObjectOfType<PlayerShip>();
        _enemiesInstantiated = CombatManager.Instance.enemiesInstantiated;
    }

    public override void Tick()
    {
        if(enemy.IsUnityNull()) return;
        
        weaponDamage = _enemyShip.HasPsionicShield ? weaponDamage / 2 : weaponDamage;

        switch (weaponType)
        {
            case WeaponType.Laser:
                if (_enemyShip.HasPlasmaShield)
                    _playerShip.healthManager.TakeDamage(_enemyShip.TemporaryHealth);
                if (_enemyShip.HasPsionicShield)
                    _enemyShip.healthManager.TakeDamage(weaponDamage / 2);
                else _enemyShip.healthManager.TakeDamage(weaponDamage);
                break;
            
            case WeaponType.PlasmaThrower:
                foreach (GameObject enemyShip in _enemiesInstantiated)
                {
                    if (enemyShip.GetComponent<EnemyShip>().HasPlasmaShield)
                        _playerShip.healthManager.TakeDamage(enemyShip.GetComponent<EnemyShip>().TemporaryHealth);
                    if (enemyShip.GetComponent<EnemyShip>().HasPsionicShield)
                        enemyShip.GetComponent<HealthManager>().TakeShieldDamage(weaponDamage / 2);
                    else enemyShip.GetComponent<HealthManager>().TakeDamage(weaponDamage);
                }
                break;
            
            case WeaponType.Disruptor:
                if (_enemyShip.TemporaryHealth != 0)
                {
                    if (_enemyShip.HasPlasmaShield)
                        _playerShip.healthManager.TakeDamage(_enemyShip.TemporaryHealth);
                    if (_enemyShip.HasPsionicShield)
                        _enemyShip.healthManager.TakeShieldDamage(weaponDamage / 2);
                    else _enemyShip.healthManager.TakeShieldDamage(weaponDamage);
                }
                break;
            
            case WeaponType.ArcEmitter:
                foreach (GameObject enemyShip in _enemiesInstantiated.Where(enemyShip => enemyShip.GetComponent<EnemyShip>().TemporaryHealth != 0))
                {
                    if (enemyShip.GetComponent<EnemyShip>().HasPlasmaShield)
                        _playerShip.healthManager.TakeDamage(enemyShip.GetComponent<EnemyShip>().TemporaryHealth);
                    if (enemyShip.GetComponent<EnemyShip>().HasPsionicShield)
                        enemyShip.GetComponent<HealthManager>().TakeShieldDamage(weaponDamage / 2);
                    else enemyShip.GetComponent<HealthManager>().TakeDamage(weaponDamage);
                }
                break;
            
            case WeaponType.Autocannon:
                int randomEnemy = Random.Range(0, _enemiesInstantiated.Count);
                for (int i = 0; i < 5; i++)
                {
                    _enemiesInstantiated[randomEnemy].GetComponent<HealthManager>().TakePiercingDamage(weaponDamage);
                }
                break;
            
            case WeaponType.Missiles:
                if (_enemyShip.HasPlasmaShield)
                    _playerShip.healthManager.TakeDamage(_enemyShip.TemporaryHealth);
                if (_enemyShip.HasPsionicShield)
                    _enemyShip.healthManager.TakeDamage(weaponDamage / 2);
                else if (_enemyShip.TemporaryHealth == 0)
                    _enemyShip.healthManager.TakeDamage(weaponDamage * 2);
                else _enemyShip.healthManager.TakeDamage(weaponDamage);
                break;
            
            case WeaponType.Torpedoes:
                foreach (GameObject enemyShip in _enemiesInstantiated)
                {
                    if (enemyShip.GetComponent<EnemyShip>().HasPlasmaShield)
                        _playerShip.healthManager.TakeDamage(enemyShip.GetComponent<EnemyShip>().TemporaryHealth);
                    if (enemyShip.GetComponent<EnemyShip>().HasPsionicShield)
                        enemyShip.GetComponent<HealthManager>().TakeDamage(weaponDamage / 2);
                    else if (enemyShip.GetComponent<EnemyShip>().TemporaryHealth == 0)
                        enemyShip.GetComponent<HealthManager>().TakeDamage(weaponDamage * 2);
                    else enemyShip.GetComponent<HealthManager>().TakeDamage(weaponDamage);
                }
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    

    public override void Reset()
    {
        base.Reset();
        isTicked = false;
        enemy = null;
    }

}