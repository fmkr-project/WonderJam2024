using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;

public class RebirthUpdateEther : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Ether;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ether.text = GameManager.ether.ToString();
    }
}
