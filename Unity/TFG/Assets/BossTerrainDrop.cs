using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTerrainDrop : MonoBehaviour
{
    public GameObject pared;
    public GameObject pared2;


    private void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("Player"))
        {
            pared.SetActive(false);
            pared2.SetActive(false);
        }
    }
}
