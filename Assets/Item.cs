using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static int TotCollected;
    public GameObject WinScreen;
    // Start is called before the first frame update
    void Start()
    {
        WinScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (TotCollected == 3)
        {
            WinScreen.SetActive(true);
        }
    }
}