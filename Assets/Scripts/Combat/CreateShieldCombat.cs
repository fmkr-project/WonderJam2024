using System.Collections;
using System.Collections.Generic;
using Modules;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Module = System.Reflection.Module;

public class CreateShieldCombat : MonoBehaviour
{
    [SerializeField] private Image _sprite;
    [SerializeField] private TextMeshProUGUI _shield;
    [SerializeField] private TextMeshProUGUI _crew;
    [SerializeField] private TextMeshProUGUI _name;

    public void Create(Shield module)
    {
        _sprite.sprite = module.Sprite;
        _shield.text = module.ShieldHealth.ToString();
        _crew.text = module.RequiredCrew.ToString();
        _name.text = module.ModuleName;

        ShieldInCombat stats = GetComponent<ShieldInCombat>();
        stats.squareValue = module.RequiredCrew;
        stats.shieldHealth = module.ShieldHealth;
        stats.shieldType = module.ShieldType;
    }
    
}
