using System;
using System.Collections;
using System.Collections.Generic;
using Ships;
using UnityEngine;


public static class GameManager
{
    
    public static int crewCount;  
    public static int moneyAmount;

    public static PlayerShip CurrentPlayerShip { get; set; }
    
    // Temp
    public static void SaveData()
    {
        PlayerPrefs.SetInt("CrewCount", crewCount); 
        PlayerPrefs.SetInt("MoneyAmount", moneyAmount); 
        PlayerPrefs.Save();
        Debug.Log("Données sauvegardées.");
    }

   
    public static void LoadData()
    {
        crewCount = PlayerPrefs.GetInt("CrewCount", 0); 
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount", 0); 
        Debug.Log("Données chargées : Équipage = " + crewCount + ", Argent = " + moneyAmount);
    }

    
    public static void ResetData()
    {
        PlayerPrefs.DeleteKey("CrewCount"); 
        PlayerPrefs.DeleteKey("MoneyAmount"); 
        Debug.Log("Données réinitialisées.");
    }
}
