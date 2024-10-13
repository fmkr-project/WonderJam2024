using System;
using System.Collections;
using System.Collections.Generic;
using Modules;
using Ships;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyInCombat : MonoBehaviour
{
    private PlayerShip _playerShip;
    private Ship _ship;

    private void Start()
    {
        _ship = gameObject.GetComponent<Ship>();
        GetComponent<SpriteRenderer>().sprite = _ship.Sprite;
        _playerShip = FindObjectOfType<PlayerShip>();
        FindObjectOfType<AddsomeModules>().add();
    }

    private void OnMouseDown()
    {
        SelectModule.Instance.SelectEnemyShip(gameObject);
    }

    public void TakeAction()
    {
        _ship.TemporaryHealth = 0;
        foreach (var mod in  _ship.Modules)
        {
            switch (mod)
            {
                case(Shield) :
                    _ship.healthManager.Shield(((Shield)mod).ShieldHealth);
                    break ;
                case(Weapon) :
                    //TODO: maybe add "use of weapon"
                    _playerShip.healthManager.TakeDamage(((Weapon)mod).WeaponDamage);
                    break;
            }
        }
    }
}
