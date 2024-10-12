using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySupervisor : MonoBehaviour
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
        var temp1 = 20;
        var temp2 = 150;
        
        // Set internals to match current ship state
        _moneyAmountText.text = temp1.ToString();
        _crewAmountText.text = temp2.ToString();
    }
}
