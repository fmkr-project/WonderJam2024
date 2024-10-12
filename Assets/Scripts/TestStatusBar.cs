using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;


public class TestStatusBar : MonoBehaviour
{
    public StatusBar _statusBar;
    public void MoneyUp()
    {
        GameManager.moneyAmount += 100;
        _statusBar.UpdateUI();
    }
}
