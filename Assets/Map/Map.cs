using System;
using System.Collections;
using System.Collections.Generic;
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
        //TODO : recuperer le currentShip dans le manager + le progress
        if (currentShip.IsUnityNull())
            currentShip = start;
        currentShip.AddComponent<RedZoneKill>();
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
            //TODO : changer le currentship dans le manager + la map + le progress
        }
        select.ChangeAction();
        //TODO : ne pas faire start si on change les scenes
        Start();
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

