using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using TMPro;

public class CrewSpawner : MonoBehaviour
{
    public GameObject oneCrewPrefab; 
    public GameObject threeCrewPrefab;

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
        
        var oneCrewText = oneCrewPrefab.GetComponentInChildren<TMP_Text>();
        oneCrewText.text = shop.OneCrewCost.Quantity.ToString();

        
        var threeCrewText = threeCrewPrefab.GetComponentInChildren<TMP_Text>();
        threeCrewText.text = shop.ThreeCrewCost.Quantity.ToString();
    }
}

