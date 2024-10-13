using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public PlayerShip playerShip;
    public PlayerShipData data;
    string path = Path.Combine("C:\\Users\\super\\OneDrive\\Documents\\GitHub\\WonderJam2024\\Assets\\PlayerData", "playerShipData"+ 1 +".json");
    private void Start()
    {
        playerShip = FindObjectOfType<PlayerShip>();
    }

    public void SavePlayerShipData()
    {
        data = new PlayerShipData(playerShip.MaxHealth);
        foreach (var mod in playerShip.Modules)
        {
            switch (mod)
            {
                case Weapon mod1:
                    data.modules.Add(new ModuleData(ModuleData.ModuleType.Weapon,mod1.WeaponDamage));
                    break;
                case Shield mod1 :
                    data.modules.Add(new ModuleData(ModuleData.ModuleType.Shield,mod1.ShieldHealth));
                    break;
            }
        }
        string json = JsonUtility.ToJson(data, true);

        //File.WriteAllText(path, json);
        Debug.Log($"Data saved to: {path}");
        
        File.WriteAllText(path, json);
    }
    
    public void LoadPlayerShipData()
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("No save file found at: " + path);
            return;
        }

        // Lire le fichier JSON et désérialiser les données
        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<PlayerShipData>(json);
        print(data.modules[0].data);
        // Appliquer les données chargées au PlayerShip
        playerShip.MaxHealth = data.maxHealth;

        // Nettoyer les modules actuels et recréer les modules à partir des données chargées
        playerShip.Modules.Clear();

        foreach (var modData in data.modules)
        {
            switch (modData._type)
            {
                case ModuleData.ModuleType.Weapon:
                    var weapon = new Weapon { WeaponDamage = modData.data };
                    playerShip.Modules.Add(weapon);
                    break;
                case ModuleData.ModuleType.Shield:
                    var shield = new Shield { ShieldHealth = modData.data };
                    playerShip.Modules.Add(shield);
                    break;
            }
        }
        print(playerShip.Modules.Count);
        Debug.Log("Data loaded successfully.");
    }
}

