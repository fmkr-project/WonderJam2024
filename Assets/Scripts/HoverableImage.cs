using UnityEngine;
using UnityEngine.EventSystems;

public class HoverableImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea] public string hoverText; // Le texte personnalisé pour cette image

    // Appelé lorsque la souris entre dans l'image
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Affiche le tooltip à la position de la souris
        TooltipManager.Instance.ShowTooltip(hoverText, Input.mousePosition);
    }

    // Appelé lorsque la souris quitte l'image
    public void OnPointerExit(PointerEventData eventData)
    {
        // Masque le tooltip
        TooltipManager.Instance.HideTooltip();
    }
}