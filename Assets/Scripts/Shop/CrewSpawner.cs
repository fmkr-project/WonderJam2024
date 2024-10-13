using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CrewSpawner : MonoBehaviour
{
    public GameObject oneCrewPrefab;
    public GameObject threeCrewPrefab;
    public Color selectedColor = Color.gray;

    private Shop shop;

    void Start()
    {
        // Récupère le script Shop
        shop = FindObjectOfType<Shop>();

        if (shop != null)
        {
            UpdateCrewmateCosts();
            SetupButton(oneCrewPrefab, shop.BuyOneCrewmate);
            SetupButton(threeCrewPrefab, shop.BuyThreeCrewmates);
        }
        else
        {
            Debug.LogWarning("Shop script not found in the scene!");
        }
    }

    private void UpdateCrewmateCosts()
    {
        var oneCrewText = oneCrewPrefab.GetComponentInChildren<TMP_Text>();
        oneCrewText.text = shop.OneCrewCost.Quantity.ToString();

        var threeCrewText = threeCrewPrefab.GetComponentInChildren<TMP_Text>();
        threeCrewText.text = shop.ThreeCrewCost.Quantity.ToString();
    }

    private void SetupButton(GameObject prefab, System.Func<bool> buyFunction)
    {
        var button = prefab.GetComponent<Button>();
        var backgroundImage = prefab.GetComponent<Image>();
        var buttonText = prefab.GetComponentInChildren<TMP_Text>();

        button.onClick.AddListener(() =>
        {
            if (buyFunction())
            {
                
                button.interactable = false;
                if (backgroundImage != null)
                {
                    backgroundImage.color = selectedColor;
                }
                if (buttonText != null)
                {
                    buttonText.color = Color.white;
                }
            }
            else
            {
                Debug.LogWarning("Insufficient resources for purchase!");
            }
        });
    }
}