using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour

{

    public GameObject intelligence1;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            intelligence1.SetActive(false);
        }
                
    }
}
