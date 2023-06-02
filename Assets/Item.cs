using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static int TotCollected;
    public GameObject WinScreen;
    public GameObject player;
    private bool touchingEndTrigger = false;
    public GameObject eyes;
    // Start is called before the first frame update
    void Start()
    {
        WinScreen.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        touchingEndTrigger = true;
    }
    
    void Update()
    {
        if (TotCollected == 3 && touchingEndTrigger == true)
        {
            eyes.SetActive(false);
            WinScreen.SetActive(true);          
            Debug.Log("ad");
        }
    }
}
