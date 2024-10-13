using System.Collections.Generic;
using System.Linq;
using Managers;
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
        var selectedModules = modules.OrderBy(x => Random.value).Take(4).ToList();
        
        var moduleManager = FindObjectOfType<ModuleManager>();
        foreach (var module in selectedModules)
        {
            GameObject moduleInstance = Instantiate(modulePrefab, content);
            
            
            var imageComponent = moduleInstance.GetComponent<Image>();
            imageComponent.sprite = module.Sprite;
            
            var textComponent = moduleInstance.transform.Find("PriceText").GetComponent<TMP_Text>();
            textComponent.text = module.Price.Quantity.ToString();
            
            var buyButton = moduleInstance.GetComponent<Button>();
            
            buyButton.onClick.AddListener(() =>
            {
                bool purchaseSuccessful = moduleManager.BuyModule(module);
                if (purchaseSuccessful)
                {
                    Destroy(moduleInstance);
                }
                        
            });
        }
    }
}

