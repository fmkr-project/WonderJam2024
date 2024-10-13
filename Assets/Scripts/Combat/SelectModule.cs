using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Ships;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Upgrades;
using Image = UnityEngine.UI.Image;


public class SelectModule : MonoBehaviour
{
    
    public static SelectModule Instance { get; private set; }
    public int maxTotal = 4; //TODO change this value to match the real value

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


    private void Update()
    {
        if (_enemyInCombats.IsUnityNull()||_enemyInCombats.Length==0)
            _enemyInCombats = FindObjectsOfType<EnemyInCombat>();
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
        StartCoroutine(enemyinCombat.FlashDamageEffect(_ship));
        _selectedWeapon = null;
        foreach (var enemyInCombat in _enemyInCombats)
        {
            enemyInCombat.circle.SetActive(false);
        }
    }

    public void ResetPeople(int max)
    {
        _currentTotal = 0;
        maxTotal = max;
    }
}
