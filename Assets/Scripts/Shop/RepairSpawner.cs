using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RepairSpawner : MonoBehaviour
{
    public GameObject tenPercent; 
    public GameObject fiftyPercent;
    public Color selectedColor = Color.gray;

    private Shop shop;

    void Start()
    {
        
        shop = FindObjectOfType<Shop>();

        if (shop != null)
        {
            UpdateRepairCosts();
            SetupButton(tenPercent, shop.RepairTenPercent);
            SetupButton(fiftyPercent, shop.RepairFiftyPercent);
        }
        else
        {
            Debug.LogWarning("Shop script not found in the scene!");
        }
    }

    private void UpdateRepairCosts()
    {
        
        var tenText = tenPercent.GetComponentInChildren<TMP_Text>();
        tenText.text = shop.OneCrewCost.Quantity.ToString();

        
        var fiftyText = fiftyPercent.GetComponentInChildren<TMP_Text>();
        fiftyText.text = shop.ThreeCrewCost.Quantity.ToString();
    }
    private void SetupButton(GameObject prefab, System.Func<bool> buyFunction)
    {
        var button = prefab.GetComponent<Button>();
        var backgroundImage = prefab.GetComponent<Image>();
        var buttonText = prefab.GetComponentInChildren<TMP_Text>();

        button.onClick.AddListener(() =>
        {
            if (buyFunction())
            {
                
                button.interactable = false;
                if (backgroundImage != null)
                {
                    backgroundImage.color = selectedColor;
                }
                if (buttonText != null)
                {
                    buttonText.color = Color.white;
                }
            }
            else
            {
                Debug.LogWarning("Insufficient resources for purchase!");
            }
        });
    }
}


