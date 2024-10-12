using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : Place
{
    // private void Start()
    // {
    //     GetComponent<SpriteRenderer>().color = Color.red;
    // }

    public override void ChangeAction()
    {
        print("change scene to combat");
        //change scene to shop
        //SceneManager.LoadScene("Combat");
    }
}
