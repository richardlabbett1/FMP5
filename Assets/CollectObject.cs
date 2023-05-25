
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.SceneManagement;

public class CollectObject : MonoBehaviour
{
    public bool collected;
    [SerializeField]
    GameObject Player;
    GameObject Object;
    public int Value;
    public bool furniture;
    
    public GameObject UI;

    void Start()
    {
        Item.TotCollected = 0;
        if (!furniture)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            collected = false;
            Object = this.gameObject;
            Object.SetActive(true);
        }
        else
        {
            enabled = false;
        }
    }
    void OnMouseOver()
    {
        if (Player)
        {
            float dist = Vector3.Distance(Player.transform.position, transform.position);
            if (dist < 10)
            {
                if (collected == false)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        StartCoroutine(Collect());
                    }
                }
                else
                    return;
            }
        }
    }
    IEnumerator Collect()
    {
        Debug.Log("You Collected" + this.name);
        collected = true;
        yield return new WaitForSeconds(.5f);
        {
            Item.TotCollected++;
            Debug.Log("CollectAppli");
            Object.SetActive(false);





            Object.SetActive(false);
        }
    }
        
}