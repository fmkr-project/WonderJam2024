using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
        SceneManager.LoadScene("Scenes/ShopScene");
    }

    public void Map()
    {
        SceneManager.LoadScene(GameManager.map);
    }
}
