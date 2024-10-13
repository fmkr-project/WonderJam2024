using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Ships;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private PlayerShip _playerShip;
    private healthBar _healthBar;

    private void Start()
    {
        _playerShip = FindObjectOfType<PlayerShip>();
        _healthBar = FindObjectOfType<healthBar>();
    }

    public void Jouer()
    {
        SceneManager.LoadScene("Scenes/SceneCombatDebut");
    }
    public void Quit()
    {
       Application.Quit(); 
    }

    public void NouvelleRun()
    {
        _playerShip = FindObjectOfType<PlayerShip>();
        _playerShip.GetComponent<SlowMove>().enabled = false;
        SceneManager.LoadScene("Scenes/ShopScene");
    }

    public void Map()
    {
        SceneManager.LoadScene(GameManager.map);
    }
    
    public void  NouveauRunner()
    {
        SceneManager.LoadScene("Scenes/Tuto");
    }
    
    
}
