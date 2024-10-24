using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Ships;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Upgrades;

public class Map : MonoBehaviour
{
    public static Map Instance { get; private set; }
    public Place select;
    public Canvas goButton;
    public GameObject currentShip;
    public GameObject start;
    public GameObject finish;
    public float progress=5;
    public GameObject redZone;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this; 
        //DontDestroyOnLoad(gameObject); 
    }

    private PlayerShip ship;
    private void Start()
    {
        if (GameManager.currentShipPosition==Vector3.zero)
            GameManager.currentShipPosition = start.transform.position;
        currentShip = findCurrent();
        ship = FindObjectOfType<PlayerShip>();
        ship.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        ship.AddComponent<RedZoneKill>();
        ship.gameObject.transform.position = GameManager.currentShipPosition;
        redZone.transform.localPosition = new Vector3(progress, 0, 0);
        Connect.Instance.current = currentShip;
        Connect.Instance.MakeDistance();
        progress = GameManager.progress;
    }

    private GameObject findCurrent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GameManager.currentShipPosition, 0.1f);
        GameObject foundObject;
        if (colliders.Length > 0)
        {
            foundObject = colliders[0].gameObject;
        }
        else
        {
            foundObject = start;
            Debug.Log("No object found at the given position.");
        }

        return foundObject;
    }

    public void Go()
    {
        //currentShip.GetComponent<RedZoneKill>().enabled = false;
        currentShip = select.gameObject;
        if (currentShip == finish)
        {
            print("BossFight !");
            GameManager.progress = 0;
            GameManager.currentShipPosition = Vector3.zero;
            SceneManager.LoadScene("SceneCombatBoss");
            //changer le currentship dans le manager + la map + le progress
        }
        else
        {
            int buffer = 1;
            foreach (RebirthUpgrade upgrade in GameManager.RebirthUpgrades)
            {
                if (upgrade.Name == "LessRedzone")
                {
                    buffer++;
                }
            }

            GameManager.progress = progress+(float)0.3/buffer;
            GameManager.currentShipPosition = currentShip.transform.position;
        

            select.ChangeAction();
        }
    }


    void Update()
    {
        progress += Time.deltaTime *(float) 0.06; 
        redZone.transform.localPosition =new Vector3(progress, 0, 0);
        //check if the mouse click on nothing
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics2D.GetRayIntersection(ray))
                OnClickEmptySpace();
        }
    }

    private void OnClickEmptySpace()
    {
        goButton.gameObject.SetActive(false);
    }
}

