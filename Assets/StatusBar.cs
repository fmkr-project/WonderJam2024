using System.Collections;
using System.Collections.Generic;
using Managers;
using Ships;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    private TextMeshProUGUI _moneyAmountText;
    private TextMeshProUGUI _crewAmountText;
    private TextMeshProUGUI _scrapsAmountText;
    private TextMeshProUGUI _etherAmountText;

    void Start()
    {
        _moneyAmountText = GameObject.Find("MoneyAmount").GetComponent<TextMeshProUGUI>();
        _crewAmountText = GameObject.Find("CrewAmount").GetComponent<TextMeshProUGUI>();
        _scrapsAmountText = GameObject.Find("ScrapsAmount").GetComponent<TextMeshProUGUI>();
        _etherAmountText = GameObject.Find("EtherAmount").GetComponent<TextMeshProUGUI>();
        
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }
    
    public void UpdateUI()
    {
        var playerShip = GameManager.CurrentPlayerShip;
    }
}
