using System.Collections;
using System.Collections.Generic;
using Managers;
using Ships;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fantomes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<ShipDataHandler>().LoadPlayerShipData();
        EnemyShip enemyShip = FindObjectOfType<EnemyShip>();
        enemyShip.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f,1);
        enemyShip.gameObject.transform.rotation = Quaternion.Euler(0,0,90);
    }

    public void End()
    {
        SceneManager.LoadScene("SceneCombatFantome");
        if (GameManager.CurrentRun == GameManager.currentBossRush)
            SceneManager.LoadScene("End");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
