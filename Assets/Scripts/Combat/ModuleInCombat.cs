using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ModuleInCombat : MonoBehaviour
{
    
    public int squareValue; 
    public bool isUsed = false;

    public virtual void Tick()
    {
        
    }

    public virtual void Reset()
    {
        isUsed = false;
        GetComponent<Image>().color = Color.white;
    }
}
