using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Modules;
using ScriptableObjects.Scripts;
using UnityEngine;
using Random = System.Random;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShieldModuleScriptableObject> shieldModulesList = new();
    [SerializeField] private List<WeaponModuleScriptableObject> weaponModulesList = new();
    public List<Module> SoldModules = new();
    public ResourceAmount OneCrewCost;
    public ResourceAmount ThreeCrewCost;
    public ResourceAmount TenPercentRepairsCost;
    public ResourceAmount FiftyPercentRepairsCost;
    
    void Start()
    {
        var rng = new Random();
        var moduleList = ModuleManager.ListOfAllModules;
        var soldModulesAmount = rng.Next(3, 4);
        // Choose a random selection of modules
        SoldModules = Enumerable
            .Range(0, soldModulesAmount)
            .Select(i => rng.Next(0, 1 + moduleList.Count - soldModulesAmount))
            .OrderBy(i => i)
            .Select((a, b) => moduleList[a + b])
            .ToList();
        
        // Initialize crew / repair cost
        OneCrewCost = new ResourceAmount(Resource.Money, 1000);
        ThreeCrewCost = new ResourceAmount(Resource.Money, 2700);
        
        TenPercentRepairsCost = new ResourceAmount(Resource.Money, 250);
        FiftyPercentRepairsCost = new ResourceAmount(Resource.Money, 1100);
        
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
    }

    void BuyOneCrewmate()
    {
        var player = GameManager.CurrentPlayerShip;

        if (player.ChecksIfPlayerHasEnoughOfTheGivenResource(OneCrewCost))
        {
            player.RemoveResourceFromInventory(OneCrewCost.Resource, OneCrewCost.Quantity);
        }
    }
    
    void BuyThreeCrewmates()
    {
        var player = GameManager.CurrentPlayerShip;

        if (player.ChecksIfPlayerHasEnoughOfTheGivenResource(ThreeCrewCost))
        {
            player.RemoveResourceFromInventory(ThreeCrewCost.Resource, ThreeCrewCost.Quantity);
        }
    }

    void RepairTenPercent()
    {
        var player = GameManager.CurrentPlayerShip;
        
        if (player.ChecksIfPlayerHasEnoughOfTheGivenResource(TenPercentRepairsCost))
        {
            player.RemoveResourceFromInventory(TenPercentRepairsCost.Resource, TenPercentRepairsCost.Quantity);
        }
    }
    
    void RepairFiftyPercent()
    {
        var player = GameManager.CurrentPlayerShip;
        
        if (player.ChecksIfPlayerHasEnoughOfTheGivenResource(FiftyPercentRepairsCost))
        {
            player.RemoveResourceFromInventory(FiftyPercentRepairsCost.Resource, FiftyPercentRepairsCost.Quantity);
        }
    }
}
