using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public string firstLevel;
    public string secondLevel;
    public string thirdLevel;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Level1()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void Level2()
    {
        SceneManager.LoadScene(secondLevel);

    }

    public void Level3()
    {
        SceneManager.LoadScene(thirdLevel);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("ad");
    }












}
