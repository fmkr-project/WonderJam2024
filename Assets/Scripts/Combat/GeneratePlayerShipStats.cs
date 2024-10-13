using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Modules;
using ScriptableObjects.Scripts;
using Ships;
using Unity.VisualScripting;
using UnityEngine;
using Upgrades;

public class GeneratePlayerShipStats : MonoBehaviour
{
    [SerializeField] private EnemyShipScriptableObject enemyShipScriptableObject;
    [SerializeField] private int money;
    [SerializeField] private int crew;

    private ModuleManager moduleManager;

    private PlayerShip _playerShip;
    // Start is called before the first frame update
    void Start()
    {
        _playerShip = FindObjectOfType<PlayerShip>();
        moduleManager = _playerShip.moduleManager;
        foreach (RebirthUpgrade upgrade in GameManager.RebirthUpgrades)
        {
            if (upgrade.Name == "MoreCrew")
            {
                crew++;
            }

            if (upgrade.Name == "MoreHealth")
            {
                enemyShipScriptableObject.maxHealth += 25;
            }
        }
        PlayerShipInitialisation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    protected void PlayerShipInitialisation()
    {
        if (enemyShipScriptableObject == null)
        {
            Debug.LogError("EnemyShipScriptableObject is not assigned.");
            return;
        }

        if (moduleManager == null)
        {
            Debug.LogError("ModuleManager is not initialized.");
            return;
        }
        _playerShip.AddResourceToInventory(Resource.Money,money);
        _playerShip.AddResourceToInventory(Resource.Crew,crew);
            
        _playerShip.Health = enemyShipScriptableObject.health;
        _playerShip.MaxHealth = enemyShipScriptableObject.maxHealth;
        _playerShip.TemporaryHealth = enemyShipScriptableObject.temporaryHealth;
        _playerShip.Sprite = enemyShipScriptableObject.sprite;
            
        foreach (Shield shield in enemyShipScriptableObject.shieldModules.Select(shieldModule => new Shield(
                     shieldModule.moduleName, 
                     shieldModule.sprite,
                     shieldModule.requiredCrew,
                     shieldModule.price,
                     shieldModule.shieldType,
                     shieldModule.shieldHealth
                 )))
        {
            moduleManager.AddModuleToShip(shield);
        }
            
        foreach (Weapon weapon in enemyShipScriptableObject.weaponModules.Select(weaponModule => new Weapon(
                     weaponModule.moduleName,
                     weaponModule.sprite,
                     weaponModule.requiredCrew,
                     weaponModule.price,
                     weaponModule.weaponType,
                     weaponModule.weaponDamage
                 )))
        {
            moduleManager.AddModuleToShip(weapon);
        }
    }
}
