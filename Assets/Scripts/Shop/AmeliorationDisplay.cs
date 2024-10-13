using System.Collections.Generic;
using System.Linq;
using Managers;
using Modules;
using Ships;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AmeliorationDisplay : MonoBehaviour
{
    public GameObject AmilPrefab; 
    public Transform content; 
    private Shop shop;
    private PlayerShip _ship;

    void Start()
    {
        _ship = FindObjectOfType<PlayerShip>();
        
        shop = FindObjectOfType<Shop>();
        
        if (shop != null)
        {
            
            DisplayModules(shop.SoldAmeliorations);
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
            GameObject moduleInstance = Instantiate(AmilPrefab, content);
            
            
            var imageComponent = moduleInstance.GetComponent<Image>();
            imageComponent.sprite = module.Sprite;
            
            var textComponent = moduleInstance.transform.Find("PriceText").GetComponent<TMP_Text>();
            textComponent.text = module.Price.Quantity.ToString();
            
            var buyButton = moduleInstance.GetComponent<Button>();
            
            buyButton.onClick.AddListener(() =>
            {
                bool purchaseSuccessful = shop.ReplaceTier1WithTier2(module);
                if (purchaseSuccessful)
                {
                    Destroy(moduleInstance);
                }
            });
        }
    }
}

