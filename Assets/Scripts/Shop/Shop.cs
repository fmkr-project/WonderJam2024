using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Modules;
using ScriptableObjects.Scripts;
using Ships;
using UnityEngine;
using Random = System.Random;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShieldModuleScriptableObject> shieldModulesList = new();
    [SerializeField] private List<WeaponModuleScriptableObject> weaponModulesList = new();
    [SerializeField] private List<ShieldModuleScriptableObject> shieldModulesList2 = new();
    [SerializeField] private List<WeaponModuleScriptableObject> weaponModulesList2 = new();
    public List<Module> SoldModules = new();
    public List<Module> SoldAmeliorations = new();
    public ResourceAmount OneCrewCost;
    public ResourceAmount ThreeCrewCost;
    public ResourceAmount TenPercentRepairsCost;
    public ResourceAmount FiftyPercentRepairsCost;
    
    void Awake()
    {
       
        
        // Initialize crew / repair cost
        OneCrewCost = new ResourceAmount(Resource.Money, 50);
        ThreeCrewCost = new ResourceAmount(Resource.Money, 120);
        
        TenPercentRepairsCost = new ResourceAmount(Resource.Scrap, 20);
        FiftyPercentRepairsCost = new ResourceAmount(Resource.Scrap, 70);
        
        // Initialize the shop catalog
        foreach (Shield shield in shieldModulesList.Select(shieldModule => new Shield(
                     shieldModule.moduleName,
                     shieldModule.sprite,
                     shieldModule.requiredCrew,
                     shieldModule.price,
                     shieldModule.shieldType,
                     shieldModule.shieldHealth
                 )))
        {
            SoldModules.Add(shield);
        }
        
        foreach (Weapon weapon in weaponModulesList.Select(weaponModule => new Weapon(
                     weaponModule.moduleName,
                     weaponModule.sprite,
                     weaponModule.requiredCrew,
                     weaponModule.price,
                     weaponModule.weaponType,
                     weaponModule.weaponDamage
                 )))
        {
            SoldModules.Add(weapon);
        }
        
        foreach (Shield shield in shieldModulesList.Select(shieldModule => new Shield(
                     shieldModule.moduleName,
                     shieldModule.sprite,
                     shieldModule.requiredCrew,
                     shieldModule.price,
                     shieldModule.shieldType,
                     shieldModule.shieldHealth
                 )))
        {
            SoldAmeliorations.Add(shield);
        }
        
        foreach (Weapon weapon in weaponModulesList.Select(weaponModule => new Weapon(
                     weaponModule.moduleName,
                     weaponModule.sprite,
                     weaponModule.requiredCrew,
                     weaponModule.price,
                     weaponModule.weaponType,
                     weaponModule.weaponDamage
                 )))
        {
            SoldAmeliorations.Add(weapon);
        }
    }

  
    public bool BuyOneCrewmate()
    {
        var player = FindObjectOfType<PlayerShip>();
        Debug.Log(player.Modules.Count);

        if (player.ChecksIfPlayerHasEnoughOfTheGivenResource(OneCrewCost.Resource, OneCrewCost.Quantity))
        {
            player.RemoveResourceFromInventory(OneCrewCost.Resource, OneCrewCost.Quantity);
            GameManager.CurrentPlayerShip.Inventory[Resource.Crew]++;
            return true;
        }
        return false;
    }
    
    public bool BuyThreeCrewmates()
    {
        var player = FindObjectOfType<PlayerShip>();

        if (player.ChecksIfPlayerHasEnoughOfTheGivenResource(ThreeCrewCost.Resource, ThreeCrewCost.Quantity))
        {
            player.RemoveResourceFromInventory(ThreeCrewCost.Resource, ThreeCrewCost.Quantity);
            GameManager.CurrentPlayerShip.Inventory[Resource.Crew] += 3;
            return true;
        }
        return false;
    }

    public bool RepairTenPercent()
    {
        var player = FindObjectOfType<PlayerShip>();
            
        if (player.ChecksIfPlayerHasEnoughOfTheGivenResource(TenPercentRepairsCost.Resource, TenPercentRepairsCost.Quantity))
        {
            player.RemoveResourceFromInventory(TenPercentRepairsCost.Resource, TenPercentRepairsCost.Quantity);
            //TODO Rendre 10% des pvs du ship
            return true;
        }

        return false;
    }
    
    public bool RepairFiftyPercent()
    {
        var player = FindObjectOfType<PlayerShip>();
            
        if (player.ChecksIfPlayerHasEnoughOfTheGivenResource(FiftyPercentRepairsCost.Resource, FiftyPercentRepairsCost.Quantity))
        {
            player.RemoveResourceFromInventory(FiftyPercentRepairsCost.Resource, FiftyPercentRepairsCost.Quantity);
            //TODO Rendre  50 % des pvs du ship
            return true;
        }

        return false;
    }
        
    
    public bool ReplaceTier1WithTier2(Module module)
    {
        // Recherche de l'index du module de tier 1 correspondant dans SoldModules
        int index = SoldModules.FindIndex(m => m.ModuleName == module.ModuleName);
        if (index != -1)
        {
            // Cherche le module de tier 2 correspondant dans SoldAmeliorations
            var tier2Module = SoldAmeliorations.FirstOrDefault(m => m.ModuleName == module.ModuleName);
        
            // Si un module de tier 2 correspondant est trouvé, le remplace
            if (tier2Module != null)
            {
                SoldModules[index] = tier2Module;
                return true; // Indique que le remplacement a réussi
            }
        }
        return false; // Aucun remplacement effectué si le module correspondant n'est pas trouvé
    }


    
    private string GetBaseName(string moduleName)
    {
        int tierIndex = moduleName.LastIndexOf("_Tier");
        return tierIndex != -1 ? moduleName.Substring(0, tierIndex) : moduleName;
    }

}
