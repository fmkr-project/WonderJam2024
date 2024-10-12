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

    private void Start()
    {
        _playerShip = FindObjectOfType<PlayerShip>();
        FindObjectOfType<AddsomeModules>().add();
    }

    private void OnMouseDown()
    {
        SelectModule.Instance.SelectEnemyShip(gameObject);
    }

    public void TakeAction()
    {
        print(gameObject);
        Ship ship = gameObject.GetComponent<Ship>();
        print(ship);
        ship.TemporaryHealth = 0;
        foreach (var mod in  ship.Modules)
        {
            switch (mod)
            {
                case(Shield) :
                    ship.healthManager.Shield(((Shield)mod).ShieldHp);
                    break ;
                case(Weapon) :
                    //TODO: maybe add "use of weapon"
                    _playerShip.healthManager.TakeDamage(((Weapon)mod).Attack);
                    break;
            }
        }
    }
}
