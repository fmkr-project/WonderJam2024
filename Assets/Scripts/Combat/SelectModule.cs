using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Image = UnityEngine.UI.Image;


public class SelectModule : MonoBehaviour
{
    
    public static SelectModule Instance { get; private set; }
    public int maxTotal = 4; //TODO change this value to match the real value

    private int _currentTotal = 0;
    private WeaponInCombat _selectedWeapon;
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
        if(_selectedWeapon.IsUnityNull()) return;
        _selectedWeapon.isTicked = false;
        _selectedWeapon.GetComponent<Image>().color = Color.white;
    }

    public void OnSelect(GameObject target)
    {
        print(_currentTotal);
        if (target.TryGetComponent(out ShieldInCombat shield))
        {
            if (shield.isUsed) return;
            if (_currentTotal + shield.squareValue <= maxTotal)
            {
                _currentTotal -= shield.squareValue;
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
        _selectedWeapon.isUsed = true;
        _selectedWeapon.GetComponent<Image>().color = Color.gray;
        _selectedWeapon.enemy = enemy;
        _selectedWeapon = null;
    }

    public void ResetPeople(int max)
    {
        _currentTotal = 0;
        maxTotal = max;
    }
}
