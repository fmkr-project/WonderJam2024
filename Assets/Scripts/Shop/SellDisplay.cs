using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Modules;
using Ships;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SellDisplay : MonoBehaviour
{
    public GameObject modulePrefab; 
    public Transform content; 
    private PlayerShip ship;

    void Start()
    {
        
        ship = FindObjectOfType<PlayerShip>();
        
        if (ship != null)
        {
            StartCoroutine(WaitForOneSecond());
            
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
            
            var sellButton = moduleInstance.GetComponent<Button>();
            sellButton.onClick.AddListener(() =>
            {
                moduleManager.SellModule(module);  
                Destroy(moduleInstance);           
            });
        }
    }
    
    private IEnumerator WaitForOneSecond()
    {
        // Attend 1 seconde
        yield return new WaitForSeconds(1f);

        DisplayModules(ship.Modules.ToList());
        Debug.Log(ship.Modules.Count);
        // Code à exécuter après 1 seconde
        Debug.Log("1 seconde s'est écoulée !");
    }

}