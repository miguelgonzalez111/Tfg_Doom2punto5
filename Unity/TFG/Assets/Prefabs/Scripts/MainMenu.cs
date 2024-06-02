using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public string startScene;

    public void StartGame()
    {
        CargaNivel.NivelCarga(startScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
