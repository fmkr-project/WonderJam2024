using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Place : MonoBehaviour
{
    //open go menu 
    private void OnMouseDown()
    {
        Map.Instance.select = this;
        Map.Instance.goButton.gameObject.SetActive(true);
    }

    public abstract void ChangeAction();

}
