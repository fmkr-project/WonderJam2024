using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Ships;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class StatusBar : MonoBehaviour
{
    private TextMeshProUGUI _moneyAmountText;
    private TextMeshProUGUI _crewAmountText;
    private TextMeshProUGUI _scrapsAmountText;
    private TextMeshProUGUI _etherAmountText;
    private TextMeshProUGUI _runNumberText;

    // Ticker properties
    private TextMeshProUGUI _ticker;
    private List<string> _tickerTexts = new();
    private int _tickerSpeed = 334;

    void Awake()
    {
        _moneyAmountText = GameObject.Find("MoneyAmount").GetComponent<TextMeshProUGUI>();
        _crewAmountText = GameObject.Find("CrewAmount").GetComponent<TextMeshProUGUI>();
        _scrapsAmountText = GameObject.Find("ScrapsAmount").GetComponent<TextMeshProUGUI>();
        _etherAmountText = GameObject.Find("EtherAmount").GetComponent<TextMeshProUGUI>();
        _runNumberText = GameObject.Find("RunNumber").GetComponent<TextMeshProUGUI>();

        _ticker = GameObject.Find("Ticker").GetComponent<TextMeshProUGUI>();
        // Add some flavour text
        _tickerTexts.Add("NEV↓2.17% ERGO↓4.01% NN↑3.34% AGIV↑0.69% EYO↓1.22% UUP↑0.07% NEVE↑0.19% RGON↓1.44% NAGI↑7.02% VEY↓4.35% OUUP↓4.09%");
        _tickerTexts.Add("Federation drills panic leave hundred dead in the Outer Zneed region");
        _tickerTexts.Add("The Void pirate group to disband amidst tensions between members");
        _tickerTexts.Add("News from Far Away - Morelia district: XN-771 system's Alert Level raised from C to A+");
        _tickerTexts.Add("Abandoned Federation frigate found in the KU-0 system, investigation pending");
        _tickerTexts.Add("H'ln Spaceport opening on 12.11, first passenger services to the Greater H'l region");
        _tickerTexts.Add("Message from our sponsor: Welcome to the Danger Zone!");
        _tickerTexts.Add("This message was redacted due to a copyright claim by Elk's Emporium, Ltd.");
        _tickerTexts.Add("Q Heavy Industries quarterly report: record profits since 10 years");
        _tickerTexts.Add("If you read this message: please touch some grass");
        _tickerTexts.Add("Hurr station to open a new high-tech research wing in the near future");
        _tickerTexts.Add("Captain shot dead at Pardey Range station, locals fear retaliation");
        _tickerTexts.Add("Message from our sponsor: Up to -50% on trips to the Agg belt!");
        _tickerTexts.Add("News from Far Away - Syzk district: EKTECH acquires Two Point Technologies");
        _tickerTexts.Add("Historical eclipse to be observed on Uk'Tarr by the end of the month");
        _tickerTexts.Add("Message from our sponsor: Discover Xal, a remote pristine ternary system!");
        _tickerTexts.Add("Two Point employees on strike after merger announcement, Far East reports");
        _tickerTexts.Add("Collision between two freighters on the YL4 corridor, multiple injured");
        _tickerTexts.Add("Q signs a contract with the Za'Rr military, more than 1,500 additional ships expected");
        _tickerTexts.Add("Garde Blanche troops ambushed in Seyl-8 forest, dozens K.I.A.");
        _tickerTexts.Add("News from Far Away - Al-Taush district: Researchers discover a supermassive black hole");
        _tickerTexts.Add("News from Far Away - Shor belt: Scientific probe lost to battlefield debris");
        _tickerTexts.Add("Fierce protests on Jurong station after the execution of a Kaahl general");
        _tickerTexts.Add("Kyn Hill station engulfed in flames, passenger service diverted to Outer Kyn outpost");
        _tickerTexts.Add("EKTECH unveils its new Nightbird stealth bomber, several orders");
        _tickerTexts.Add("News from Far Away - Ketimek district: Peace treaty between Ilek and Sotmak clans");
        _tickerTexts.Add("News from Far Away - Kott district: Radio-North to cease diffusion for 14 systems");
    }
    
    void Start()
    {
        _ticker.transform.position += Vector3.right * _ticker.mesh.bounds.size.x * 2;
        SelectText();
            
        UpdateUI();
    }

    void SelectText()
    {
        _ticker.text = _tickerTexts[Random.Range(0, _tickerTexts.Count)];
    }

    void Update()
    {
        UpdateUI();
        _ticker.transform.position += Vector3.left * (_tickerSpeed * Time.deltaTime);
        if (_ticker.transform.position.x <= -_ticker.mesh.bounds.extents.x * 2)
        {
            // New text
            _ticker.transform.position += Vector3.right * (float) (_ticker.mesh.bounds.extents.x * 2);
            SelectText();
            _ticker.transform.position += Vector3.right * (float) (_ticker.mesh.bounds.extents.x * 4);
        }
    }
    
    public void UpdateUI()
    {
        var playerShip = GameManager.CurrentPlayerShip;
        _runNumberText.text = "Run " + GameManager.CurrentRun;

        try
        {
            _moneyAmountText.text = playerShip.Inventory[Resource.Money].ToString();
            _crewAmountText.text = playerShip.Inventory[Resource.Crew].ToString();
            _scrapsAmountText.text = playerShip.Inventory[Resource.Scrap].ToString();
            _etherAmountText.text = playerShip.Inventory[Resource.Ether].ToString();
        }
        catch (NullReferenceException e)
        {
            _moneyAmountText.text = "!!!";
            _crewAmountText.text = "!!!";
            _scrapsAmountText.text = "!!!";
            _etherAmountText.text = "!!!";
        }
    }
}
