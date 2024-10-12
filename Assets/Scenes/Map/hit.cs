using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    public float detectionRadius = 0.1f; // Rayon de détection
    private void Update()
    {
        // Obtenez la position du collider à vérifier
        Vector2 colliderPosition = GetComponent<Collider2D>().bounds.center;

        // Vérifiez si un autre collider est dans le cercle de détection
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(colliderPosition, detectionRadius);
        
        // Parcourez tous les colliders détectés
        foreach (var hitCollider in hitColliders)
        {
            
            if (hitCollider.gameObject.name=="RedZone")
            {
                print("hiiiiiiiiiiiiiiiiiiiiiit owi");
            }
        }

    }
}
