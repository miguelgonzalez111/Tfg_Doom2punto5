using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPitDetector : MonoBehaviour
{
    public GameObject GrupoEnemigos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GrupoEnemigos.SetActive(true);
        }
    }
}
