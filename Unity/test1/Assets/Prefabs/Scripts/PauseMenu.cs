using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string mainMenu;
    public GameObject pauseScreen;
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1.0f;
            LevelFX.instanciate.playerAS.volume = .1f;
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0.0f;
            LevelFX.instanciate.playerAS.volume = .05f;
        }
    }

    public void MainMenu()
    {
        PauseUnpause();
        SceneManager.LoadScene(mainMenu);        
    }

    public void QuitGame()
    {
        PauseUnpause();
        Application.Quit();
    }
}
