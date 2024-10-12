using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : Place
{
    
    public override void ChangeAction()
    {
        print("change scene to shop");
        //change scene to shop
        //SceneManager.LoadScene("Shop");
    }
}
