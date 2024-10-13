using System;
using System.Collections;
using System.Collections.Generic;
using Modules;
using Ships;
using Unity.VisualScripting;
using UnityEngine;
using Upgrades.Combat.Modules;
using Random = UnityEngine.Random;

public class EnemyInCombat : MonoBehaviour
{
    public GameObject circle;
    private PlayerShip _playerShip;
    private Ship _ship;
    

    private void Start()
    {
        _ship = gameObject.GetComponent<Ship>();
        GetComponent<SpriteRenderer>().sprite = _ship.Sprite;
        _playerShip = FindObjectOfType<PlayerShip>();
        //FindObjectOfType<AddsomeModules>().add();
    }

    private void OnMouseDown()
    {
        SelectModule.Instance.SelectEnemyShip(gameObject);
    }

    public void TakeAction()
    {
        _ship.TemporaryHealth = 0;
        _ship.HasPlasmaShield = false;
        _ship.HasPsionicShield = false;

        foreach (Module mod in _ship.Modules)
        {
            switch (mod)
            {
                case Shield shield :
                    switch (shield.ShieldType)
                    {
                        case ShieldType.Shield:
                            _ship.healthManager.Shield(shield.ShieldHealth);
                            break;
                        
                        case ShieldType.RegenerativeShield:
                            _ship.healthManager.Heal(shield.ShieldHealth);
                            break;
                        
                        case ShieldType.PlasmaShield:
                            _ship.HasPlasmaShield = true;
                            _ship.healthManager.Shield(shield.ShieldHealth);
                            break;
                        
                        case ShieldType.PsionicShield:
                            _ship.HasPsionicShield = true;
                            _ship.healthManager.Shield(shield.ShieldHealth);
                            break;
                        
                        default:
                            Debug.Log("Shield type not found");
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                
                case Weapon weapon :
                    
                    Sprite projectile;
                    int weaponDamage = _playerShip.HasPsionicShield ? weapon.WeaponDamage / 2 : weapon.WeaponDamage;
                    
                    if (_playerShip.HasPlasmaShield)
                    {
                        _ship.healthManager.TakeDamage(_playerShip.TemporaryHealth);
                    }
                    
                    switch (weapon.WeaponType)
                    {
                        case WeaponType.Laser:
                            projectile = Resources.Load<Sprite>("Particles (Sprites)/Beam");
                            _playerShip.healthManager.TakeDamage(weaponDamage);
                            break;
                        
                        case WeaponType.PlasmaThrower:
                            projectile = Resources.Load<Sprite>("Particles (Sprites)/Projectile Sharp");
                            _playerShip.healthManager.TakeDamage(weaponDamage);
                            break;
                        
                        case WeaponType.Disruptor:
                            projectile = Resources.Load<Sprite>("Particles (Sprites)/Small Flare");
                            if (_playerShip.TemporaryHealth == 0) continue;
                            _playerShip.healthManager.TakeShieldDamage(weaponDamage);
                            break;
                        
                        case WeaponType.ArcEmitter:
                            projectile = Resources.Load<Sprite>("Particles (Sprites)/BigFlare");
                            if (_playerShip.TemporaryHealth == 0) continue;
                            _playerShip.healthManager.TakeShieldDamage(weaponDamage);
                            break;
                        
                        case WeaponType.Autocannon:
                            projectile = Resources.Load<Sprite>("Particles (Sprites)/Projectile Thin");
                            _playerShip.healthManager.TakePiercingDamage(weaponDamage * 5);
                            break;
                        
                        case WeaponType.Missiles:
                            projectile = Resources.Load<Sprite>("Missiles/Missile (1)");
                            if (_playerShip.TemporaryHealth == 0)
                            {
                                _playerShip.healthManager.TakeDamage(weaponDamage * 2);
                            }
                            else
                            {
                                _playerShip.healthManager.TakeDamage(weaponDamage);
                            }
                            break;
                        
                        case WeaponType.Torpedoes:
                            projectile = Resources.Load<Sprite>("Missiles/Missile (3)");
                            if (_playerShip.TemporaryHealth == 0)
                            {
                                _playerShip.healthManager.TakeDamage(weaponDamage * 2);
                            }
                            else
                            {
                                _playerShip.healthManager.TakeDamage(weaponDamage);
                            }
                            break;
                        
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    // Animate projectile
                    StartCoroutine(ProjectileAnimation.Animate(projectile, transform.position, _playerShip.transform.position, 0.25f));
                    StartCoroutine(FlashDamageEffect(_playerShip));
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        _playerShip.GetComponent<SpriteRenderer>().color=Color.white;
    }

    public IEnumerator FlashDamageEffect(Ship ship)
    {
        var _spriteRenderer = ship.GetComponent<SpriteRenderer>();
        var originalColor = _spriteRenderer.color;
        var originalPosition = ship.transform.localPosition;
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color= Color.white;
        float elapsed = 0f;

        while (elapsed < 0.5f)
        {
            // Générer un déplacement aléatoire pour le vacillement
            float offsetX = Random.Range(-0.1f, 0.1f);
            float offsetY = Random.Range(-0.1f, 0.1f);
            float offsetZ = Random.Range(-0.1f, 0.1f);

            // Appliquer la position vacillante
            ship.transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, offsetZ);
            elapsed += Time.deltaTime;

            yield return null; // Attendre la prochaine frame
        }

        ship.transform.localPosition = originalPosition; // Restaurer la position initiale
    }
}
