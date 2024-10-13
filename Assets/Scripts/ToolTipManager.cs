using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance; // Singleton pour un accès global au tooltip
    public GameObject tooltip; // Référence au GameObject du tooltip
    public TMP_Text tooltipText; // Référence au texte du tooltip

    private void Awake()
    {
        // Configure le singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        tooltip.SetActive(false); // Masquer le tooltip au démarrage
    }

    // Affiche le tooltip avec le texte spécifié
    public void ShowTooltip(string message, Vector3 position)
    {
        tooltipText.text = message;
        tooltip.transform.position = position;
        tooltip.SetActive(true);
    }

    // Masque le tooltip
    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }
}