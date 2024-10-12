using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    private TextMeshProUGUI _moneyAmountText;
    private TextMeshProUGUI _crewAmountText;
    
    void Start()
    {
        // Load internals
        _moneyAmountText = GameObject.Find("MoneyAmount").GetComponent<TextMeshProUGUI>();
        _crewAmountText = GameObject.Find("CrewAmount").GetComponent<TextMeshProUGUI>();
        
        // Load ship information
        // TODO
        int temp1 = 25;
        int temp2 = 8;
        
        // Update status bar info
        _moneyAmountText.text = temp1.ToString();
        _crewAmountText.text = temp2.ToString();
    }

    void Update()
    {
        
    }
}
