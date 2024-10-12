using System.Collections.Generic;
using Modules;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ModuleDisplay : MonoBehaviour
{
    public GameObject modulePrefab; 
    public Transform content; 
    private Shop shop;

    void Start()
    {
        
        shop = FindObjectOfType<Shop>();
        
        if (shop != null)
        {
            
            DisplayModules(shop.SoldModules);
        }
        else
        {
            Debug.LogWarning("Shop script not found in the scene!");
        }
    }

    private void DisplayModules(List<Module> modules)
    {
        foreach (var module in modules)
        {
            GameObject moduleInstance = Instantiate(modulePrefab, content);
            
            
            var imageComponent = moduleInstance.GetComponent<Image>();
            imageComponent.sprite = module.Sprite;
            
            var textComponent = moduleInstance.transform.Find("PriceText").GetComponent<TMP_Text>();
            textComponent.text = module.Price.Quantity.ToString();
        }
    }
}