using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Combat : Place
{
    // private void Start()
    // {
    //     GetComponent<SpriteRenderer>().color = Color.red;
    // }

    public override void ChangeAction()
    {
        print("change scene to combat");
        SceneManager.LoadScene("SceneCombat");
        //change scene to shop
        //SceneManager.LoadScene("Combat");
    }
}
