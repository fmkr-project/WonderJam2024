using System.Collections;
using System.Collections.Generic;
using Modules;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Module = System.Reflection.Module;

public class CreateWeaponCombat : MonoBehaviour
{
    [SerializeField] private Image _sprite;
    [SerializeField] private TextMeshProUGUI _atk;
    [SerializeField] private TextMeshProUGUI _crew;
    [SerializeField] private TextMeshProUGUI _name;

    public void Create(Weapon module)
    {
        _sprite.sprite = module.Sprite;
        _atk.text = module.WeaponDamage.ToString();
        _crew.text = module.RequiredCrew.ToString();
        _name.text = module.ModuleName;

        WeaponInCombat stats = GetComponent<WeaponInCombat>();
        stats.squareValue = module.RequiredCrew;
        stats.weaponDamage = module.WeaponDamage;
        stats.weaponType = module.WeaponType;
    }
    
}
