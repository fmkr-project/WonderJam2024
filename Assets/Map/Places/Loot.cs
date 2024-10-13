using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loot : Place
{
    // private void Start()
    // {
    //     GetComponent<SpriteRenderer>().color = Color.yellow;
    // }
    public override void ChangeAction()
    {
        print("change scene to loot");
        SceneManager.LoadScene("Asteroides");
        //change scene to shop
        //SceneManager.LoadScene("Loot");
    }
}
