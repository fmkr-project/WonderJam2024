using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Ships;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

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

    private void Start()
    {
        currentShip = GameManager.currentShipPosition;
        progress = GameManager.progress;
        if (currentShip.IsUnityNull())
            currentShip = start;
        currentShip.AddComponent<RedZoneKill>();
        FindObjectOfType<PlayerShip>().gameObject.transform.position = currentShip.transform.position;
        redZone.transform.localPosition = new Vector3(progress, 0, 0);
        Connect.Instance.current = currentShip;
        Connect.Instance.MakeDistance();
    }

    public void Go()
    {
        currentShip.GetComponent<RedZoneKill>().enabled = false;
        currentShip = select.gameObject;
        if (select.gameObject == finish)
        {
            GameManager.progress = 0;
            GameManager.currentShipPosition = null;
            //TODO : changer le currentship dans le manager + la map + le progress
        }
        select.ChangeAction();
    }


    void Update()
    {
        progress += Time.deltaTime *(float) 0.08; 
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

