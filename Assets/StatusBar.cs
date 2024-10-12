using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StatusBar : MonoBehaviour
{

    
    
    public TMP_Text crewText; 
    public TMP_Text moneyText; 
    
    public void UpdateUI()
    {
        crewText.text = GameManager.crewCount.ToString();
        moneyText.text = GameManager.moneyAmount.ToString();
    }

    
    private void Start()
    {
        UpdateUI(); 
}

}
