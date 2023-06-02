using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public string Menu;
    public GameObject playerCanvas;

    [SerializeField] GameObject gameObject;
   

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
        playerCanvas.SetActive(true);
        isGamePaused = false;
    }
    void PauseGame()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
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
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("help");
    }






}
