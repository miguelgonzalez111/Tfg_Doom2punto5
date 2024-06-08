using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBossTrigger : MonoBehaviour
{
    public GameObject pared;
    public GameObject pared2; // El objeto que queremos activar/desactivar

    public Vector3 boxSize = new Vector3(10f, 10f, 10f); // Tamaño de la caja de detección
    public string enemyTag = "Enemy"; // Tag del enemigo a detectar

    private bool enemyDetected = false; // Inicializar la variable

    void Update()
    {
        DetectEnemies();
    }

    void DetectEnemies()
    {
        Vector3 detectionCenter = transform.position; // Centro de la caja de detección es la posición del objeto
        Collider[] hitColliders = Physics.OverlapBox(detectionCenter, boxSize / 2, Quaternion.identity);
        bool enemyFound = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(enemyTag))
            {
                enemyFound = true;
                break;
            }
        }

        if (enemyFound && !enemyDetected)
        {
            enemyDetected = true;
        }
        else if (!enemyFound && enemyDetected)
        {
            enemyDetected = false;
            ActivatePared();
        }
    }

    // Dibujar la caja de detección en la vista de escena para visualización
    void OnDrawGizmosSelected()
    {
        Vector3 detectionCenter = transform.position; // Centro de la caja de detección es la posición del objeto
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectionCenter, boxSize);
    }

    private void ActivatePared()
    {
        pared.SetActive(true);
        pared2.SetActive(true);
    }

    




}
