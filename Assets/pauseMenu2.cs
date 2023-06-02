using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class pauseMenu2 : MonoBehaviour
{
    public static bool isGamePaused = false;
    public string Menu;
    public GameObject playerCanvas;
    public GameObject ThePlayer;

    public GameObject gameObject;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

    }
    void ResumeGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameObject playerObject = GameObject.Find("Player");
        FirstPersonController firstPersonController = playerObject.GetComponent<FirstPersonController>();
        if (firstPersonController != null)
        {
            firstPersonController.enabled = false;
        }

        playerCanvas.SetActive(true);
        isGamePaused = false;
    }
    void PauseGame()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameObject.Find("Player").GetComponent<FirstPersonController>().enabled = false;
        playerCanvas.SetActive(false);
        isGamePaused = true;
    }

    public void resumeGame()
    {
        ResumeGame();
    }
    public void mainMenu()
    {
        SceneManager.LoadScene(Menu);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("help");
    }






}
