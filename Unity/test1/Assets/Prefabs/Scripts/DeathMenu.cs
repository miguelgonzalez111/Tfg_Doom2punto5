using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public float waitTime;
    public string mainMenu;
    public string currentScene;
    public GameObject player;
    public GameObject deathMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.activeInHierarchy)
        {
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(waitTime);
        if (!deathMenu.activeSelf)
        {
            deathMenu.gameObject.SetActive(true);
            LevelFX.instanciate.playMenuMusic(LevelFX.instanciate.deathMenuSong);
        }
    }

    //Esto es provisional hasta los checkpoints
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
