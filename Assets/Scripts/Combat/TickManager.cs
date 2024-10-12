using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TickManager : MonoBehaviour
{
    public TMP_Text valueText; 
    public Button square;
    public int maxTotal = 4;

    private int squareValue; 
    private int currentTotal = 0; 
    private bool isTicked = false; 

    void Start()
    {
       
        squareValue = int.Parse(valueText.text);

        
        square.onClick.AddListener(OnSquareClicked);
    }

    void OnSquareClicked()
    {
        if (!isTicked) 
        {
            if (currentTotal + squareValue <= maxTotal)
            {
                isTicked = true;
                currentTotal += squareValue; 
                square.GetComponent<Image>().color = Color.green; 
            }
            else
            {
                
                Debug.Log("Le total maximum de 4 est atteint. Impossible d'ajouter cette valeur.");
            }
        }
        else 
        {
            isTicked = false;
            currentTotal -= squareValue; 
            square.GetComponent<Image>().color = Color.white;
        }

        
        
    }
}