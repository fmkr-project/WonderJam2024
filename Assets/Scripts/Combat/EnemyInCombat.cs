using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInCombat : MonoBehaviour
{
    private void OnMouseDown()
    {
        SelectModule.Instance.SelectEnemyShip(gameObject);
    }

    public void TakeAction()
    {
        //TODO : do something to the player :D 
    }
}
