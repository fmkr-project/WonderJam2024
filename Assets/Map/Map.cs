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
        print(currentShip.transform.position);
        currentShip = select.gameObject;
        print(currentShip.transform.position);
        if (currentShip == finish)
        {
            GameManager.progress = 0;
            GameManager.currentShipPosition = new Vector3();
            //TODO : changer le currentship dans le manager + la map + le progress
        }
        
        GameManager.progress = progress+(float)0.5;
        GameManager.currentShipPosition = currentShip.transform.position;
        StartCoroutine(DansUneSeconde());

        IEnumerator DansUneSeconde()
        {
            yield return new WaitForSeconds(0.5f);
            ship.transform.localScale = new Vector3(1,1,1);
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

