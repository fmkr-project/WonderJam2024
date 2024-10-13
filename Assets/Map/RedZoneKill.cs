using System;
using System.Collections;
using System.Collections.Generic;
using Ships;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedZoneKill : MonoBehaviour
{
    public float detectionRadius = 0.1f; // Rayon de détection
    private bool _gameOver;
    private void Update()
    {
        if (_gameOver) return;
        // Obtenez la position du collider à vérifier
        Vector2 colliderPosition = GetComponent<Collider2D>().bounds.center;

        // Vérifiez si un autre collider est dans le cercle de détection
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(colliderPosition, detectionRadius);
        
        // Parcourez tous les colliders détectés
        foreach (var hitCollider in hitColliders)
        {
            
            if (hitCollider.gameObject.name=="RedZone")
            {
                FindObjectOfType<PlayerShip>().ShipDeath();
                _gameOver = true;
                SceneManager.LoadScene("DeathRebirth");
            }
        }

    }
}
