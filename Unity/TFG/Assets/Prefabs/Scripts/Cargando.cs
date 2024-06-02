using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.HDROutputUtils;

public class Cargando : MonoBehaviour
{
    public Text texto;

    AsyncOperation operacion;

    private void Start()
    {
        
        string nivelACargar = CargaNivel.siguienteNivel;
        StartCoroutine(IniciarCarga(nivelACargar));
    }

    IEnumerator IniciarCarga(string nivel)
    {
        yield return new WaitForSeconds(1f);
        operacion = SceneManager.LoadSceneAsync(nivel);
        operacion.allowSceneActivation = false;

        while (!operacion.isDone)
        {
            if (operacion.progress >= 0.9f)
            {
                texto.text = "Presione cualquier tecla para continuar";
                if (Input.anyKey)
                {
                    operacion.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
