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
    private TextMeshProUGUI _runNumberText;

    // Ticker properties
    private TextMeshProUGUI _ticker;
    private string _tickerText = "Lorem ipsum dolor sit amet, lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, lorem ipsum dolor sit amet...";
    private int _tickerSpeed = 669;

    void Awake()
    {
        _moneyAmountText = GameObject.Find("MoneyAmount").GetComponent<TextMeshProUGUI>();
        _crewAmountText = GameObject.Find("CrewAmount").GetComponent<TextMeshProUGUI>();
        _scrapsAmountText = GameObject.Find("ScrapsAmount").GetComponent<TextMeshProUGUI>();
        _etherAmountText = GameObject.Find("EtherAmount").GetComponent<TextMeshProUGUI>();
        _runNumberText = GameObject.Find("RunNumber").GetComponent<TextMeshProUGUI>();

        _ticker = GameObject.Find("Ticker").GetComponent<TextMeshProUGUI>();
        _ticker.text = _tickerText;
    }
    
    void Start()
    {
        _ticker.transform.position += Vector3.right * _ticker.mesh.bounds.size.x * 2;
            
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
        _ticker.transform.position += Vector3.left * (_tickerSpeed * Time.deltaTime);
        if (_ticker.transform.position.x <= -_ticker.mesh.bounds.size.x * 2)
        {
            _ticker.transform.position += Vector3.right * (_ticker.mesh.bounds.size.x * 4);
        }
    }
    
    public void UpdateUI()
    {
        var playerShip = GameManager.CurrentPlayerShip;
        _runNumberText.text = "Run " + GameManager.CurrentRun;
        
        
    }
}
