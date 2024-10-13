using System;
using System.Collections;
using System.Collections.Generic;
using Ships;
using TMPro;
using UnityEngine;

public class healthBar : MonoBehaviour
{
    public TMP_Text Pvrestants;
    public TMP_Text Pvmax;
    public TMP_Text Pvtemp;
    private PlayerShip _playerShip;

    public string initialText = "Hello, TextMeshPro!";

    void Start()
    {
        // Vérifie que les TMP_Text sont assignés
        if (Pvrestants != null && Pvmax != null && Pvtemp != null)
        {
            // Initialise les textes avec un texte par défaut
            Pvrestants.text = initialText;
            Pvmax.text = initialText;
            Pvtemp.text = initialText;
        }
        else
        {
            Debug.LogError("Un ou plusieurs TMP_Text ne sont pas assignés dans l'inspecteur !");
        }

        // Utilise FindObjectOfType pour localiser le PlayerShip dans la scène
        _playerShip = FindObjectOfType<PlayerShip>();

        // Vérifie que le PlayerShip a bien été trouvé
        if (_playerShip == null)
        {
            Debug.LogError("Aucun PlayerShip trouvé dans la scène !");
        }
        else
        {
            // Appel de UpdateLife() pour initialiser les valeurs si PlayerShip est trouvé
            UpdateLife();
        }
    }

    public void UpdateLife()
    {
        // Vérifie que _playerShip n'est pas nul avant de mettre à jour les valeurs
        if (_playerShip != null)
        {
            Pvmax.text = _playerShip.MaxHealth.ToString();
            Pvrestants.text = _playerShip.Health.ToString();
            Pvtemp.text = _playerShip.TemporaryHealth.ToString();
        }
        else
        {
            Debug.LogError("_playerShip est nul. Assurez-vous que le composant PlayerShip est bien présent dans la scène.");
        }
    }
}