using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Modules;
using Ships;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Upgrades;
using Upgrades.Combat.Modules;
using Image = UnityEngine.UI.Image;


public class SelectModule : MonoBehaviour
{
    
    public static SelectModule Instance { get; private set; }
    public int maxTotal = 4; //this value is changed externally

    private int _currentTotal = 0;
    private WeaponInCombat _selectedWeapon;
    private EnemyInCombat[] _enemyInCombats;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject); 
    }

    public void UpdateEnemies()
    {
        if (_enemyInCombats.IsUnityNull()||_enemyInCombats.Length==0)
            _enemyInCombats = FindObjectsOfType<EnemyInCombat>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics2D.GetRayIntersection(ray))
                OnClickEmptySpace();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void OnClickEmptySpace()
    {
        foreach (var enemyInCombat in _enemyInCombats)
        {
            if(enemyInCombat.IsUnityNull()) continue;
            enemyInCombat.circle.SetActive(false);
        }
        if(_selectedWeapon.IsUnityNull()) return;
        _selectedWeapon.isTicked = false;
        _selectedWeapon.GetComponent<Image>().color = Color.white;
    }

    public void OnSelect(GameObject target)
    {
        double buffer=1.0f;
        double bufferW = 1.0f;
        foreach (RebirthUpgrade upgrade in GameManager.RebirthUpgrades)
        {
            if (upgrade.Name == "MoreShield")
            {
                buffer = buffer*1.05f;
            }
            if (upgrade.Name == "MoreDamage")
            {
                buffer = buffer*1.05f;
            }
        }

        if (target.TryGetComponent(out ShieldInCombat shield))
        {
            if (shield.isUsed) return;
            if (_currentTotal + shield.squareValue <= maxTotal)
            {
                _currentTotal += (int)(shield.squareValue*buffer);
                shield.isUsed = true;
                shield.GetComponent<Image>().color = Color.gray; 
            }
        }
        if (target.TryGetComponent(out WeaponInCombat weapon))
        {
            if (weapon.isUsed) return;
            if (!weapon.isTicked)
            {
                if (_currentTotal + weapon.squareValue <= maxTotal)
                {
                    foreach (var enemyInCombat in _enemyInCombats)
                    {
                        if(enemyInCombat.IsUnityNull())continue;
                        enemyInCombat.circle.SetActive(true);
                    }
                    _selectedWeapon = weapon;
                    weapon.isTicked = true;
                    target.GetComponent<Image>().color = Color.green; 
                }
            }
            else 
            {
                weapon.isTicked = false;
                target.GetComponent<Image>().color = Color.white;
            }
        }
        
    }

    public void SelectEnemyShip(GameObject enemy)
    {
        if(_selectedWeapon.IsUnityNull()) return;
        _currentTotal += _selectedWeapon.squareValue;
        print(_selectedWeapon.squareValue);
        print(_currentTotal);
        _selectedWeapon.isUsed = true;
        _selectedWeapon.GetComponent<Image>().color = Color.gray;
        _selectedWeapon.enemy = enemy;
        var _ship = _selectedWeapon.enemy.GetComponent<EnemyShip>();
        var enemyinCombat = _selectedWeapon.enemy.GetComponent<EnemyInCombat>();
        _selectedWeapon._enemyShip = _ship;
        _selectedWeapon._enemyInCombat = enemyinCombat;

        Sprite projectile;
        switch (_selectedWeapon.weaponType)
        {
            case WeaponType.Laser:
                projectile = Resources.Load<Sprite>("Particles (Sprites)/Beam");
                break;
            case WeaponType.PlasmaThrower:
                projectile = Resources.Load<Sprite>("Particles (Sprites)/Projectile Sharp");
                break;
            case WeaponType.Disruptor:
                projectile = Resources.Load<Sprite>("Particles (Sprites)/Small Flare");
                break;
            case WeaponType.ArcEmitter:
                projectile = Resources.Load<Sprite>("Particles (Sprites)/BigFlare");
                break;
            case WeaponType.Autocannon:
                projectile = Resources.Load<Sprite>("Particles (Sprites)/Projectile Thin");
                break;
            case WeaponType.Missiles:
                projectile = Resources.Load<Sprite>("Missiles/Missile (1)");
                break;
            case WeaponType.Torpedoes:
                projectile = Resources.Load<Sprite>("Missiles/Missile (3)");
                break;
            default:
                projectile = Resources.Load<Sprite>("Particles (Sprites)/Projectile Sharp");
                break;
        }
        // Animate projectile
        StartCoroutine(ProjectileAnimation.Animate(projectile, PlayerShip.Instance.transform.position, enemy.transform.position, 0.25f));

        _selectedWeapon = null;
        foreach (var enemyInCombat in _enemyInCombats)
        {
            if(enemyInCombat.IsUnityNull()) continue;
            enemyInCombat.circle.SetActive(false);
        }
    }

    public void ResetPeople(int max)
    {
        _currentTotal = 0;
        maxTotal = max;
    }
}