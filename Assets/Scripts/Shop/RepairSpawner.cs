using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using TMPro;

public class RepairSpawner : MonoBehaviour
{
    public GameObject tenPercent; 
    public GameObject fiftyPercent;

    private Shop shop;

    void Start()
    {
        
        shop = FindObjectOfType<Shop>();

        if (shop != null)
        {
            UpdateCrewmateCosts();
        }
        else
        {
            Debug.LogWarning("Shop script not found in the scene!");
        }
    }

    private void UpdateCrewmateCosts()
    {
        
        var tenText = tenPercent.GetComponentInChildren<TMP_Text>();
        tenText.text = shop.OneCrewCost.Quantity.ToString();

        
        var fiftyText = fiftyPercent.GetComponentInChildren<TMP_Text>();
        fiftyText.text = shop.ThreeCrewCost.Quantity.ToString();
    }
}

