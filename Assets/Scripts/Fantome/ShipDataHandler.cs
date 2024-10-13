using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Managers;
using Modules;
using Ships;
using UnityEngine;

[Serializable]
public class ModuleData
{
    public enum ModuleType
    {
        Weapon,
        Shield
    }

    public WeaponType _WeaponType;
    public ShieldType _ShieldType;

    public ModuleType _type;

    public int data;

    public ModuleData(ModuleType _type, int data)
    {
        this._type = _type;
        this.data = data;
    }
}

[Serializable]
public class PlayerShipData
{
    public int maxHealth;
    public List<ModuleData> modules;

    public PlayerShipData(int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.modules = new List<ModuleData>();
    }
}
public class ShipDataHandler : MonoBehaviour
{
    public PlayerShipData data;
    public void SavePlayerShipData()
    {
        string pathSave = Path.Combine(Application.dataPath +"\\PlayerData", "playerShipData"+ (GameManager.CurrentRun -1) +".json");
        PlayerShip playerShip;
        playerShip = FindObjectOfType<PlayerShip>();
        data = new PlayerShipData(playerShip.MaxHealth);
        foreach (var mod in playerShip.Modules)
        {
            switch (mod)
            {
                case Weapon mod1:
                    data.modules.Add(new ModuleData(ModuleData.ModuleType.Weapon,mod1.WeaponDamage));
                    data.modules[data.modules.Count - 1]._WeaponType = mod1.WeaponType;
                    break;
                case Shield mod1 :
                    data.modules.Add(new ModuleData(ModuleData.ModuleType.Shield,mod1.ShieldHealth));
                    data.modules[data.modules.Count - 1]._ShieldType = mod1.ShieldType;
                    break;
            }
        }
        string json = JsonUtility.ToJson(data, true);

        //File.WriteAllText(path, json);
        Debug.Log($"Data saved to: {pathSave}");
        
        File.WriteAllText(pathSave, json);
    }
    
    public void LoadPlayerShipData()
    {
        string pathLoad = Path.Combine(Application.dataPath +"\\PlayerData", "playerShipData"+ GameManager.currentBossRush +".json");
        EnemyShip enemyShip = FindObjectOfType<EnemyShip>();
        if (!File.Exists(pathLoad))
        {
            Debug.LogWarning("No save file found at: " + pathLoad);
            return;
        }

        // Lire le fichier JSON et désérialiser les données
        string json = File.ReadAllText(pathLoad);
        data = JsonUtility.FromJson<PlayerShipData>(json);
        print(data.modules[0].data);
        // Appliquer les données chargées au PlayerShip
        enemyShip.MaxHealth = data.maxHealth;

        // Nettoyer les modules actuels et recréer les modules à partir des données chargées
        enemyShip.Modules.Clear();

        foreach (var modData in data.modules)
        {
            switch (modData._type)
            {
                case ModuleData.ModuleType.Weapon:
                    var weapon = new Weapon { WeaponDamage = modData.data, WeaponType = modData._WeaponType};
                    enemyShip.Modules.Add(weapon);
                    break;
                case ModuleData.ModuleType.Shield:
                    var shield = new Shield { ShieldHealth = modData.data, ShieldType = modData._ShieldType};
                    enemyShip.Modules.Add(shield);
                    break;
            }
        }

        GameManager.currentBossRush += 1;
        print(enemyShip.Modules.Count);
        Debug.Log("Data loaded successfully.");
    }
}

