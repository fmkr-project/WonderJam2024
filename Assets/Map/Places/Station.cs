using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : Place
{
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
    }
    public override void ChangeAction()
    {
        print("change scene to shop");
        //change scene to shop
        //SceneManager.LoadScene("Shop");
    }
}
