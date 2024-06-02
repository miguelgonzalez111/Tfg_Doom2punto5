using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPitTrigger : MonoBehaviour
{
    public GameObject pared;
    public GameObject pared2; // El objeto que queremos activar/desactivar
    public float shrinkDuration = 1.0f; // Duración de la reducción de escala
    public bool isShrinking = false;

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
            ActivatePared();
        }
        else if (!enemyFound && enemyDetected)
        {
            enemyDetected = false;
            StartCoroutine(DeactivatePared());
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
        pared.transform.localScale = Vector3.one; // Restablecer la escala a 1
        pared2.SetActive(true);
        pared2.transform.localScale = Vector3.one; // Restablecer la escala a 1
    }

    private IEnumerator DeactivatePared()
    {
        isShrinking = true;
        Vector3 originalScale = pared.transform.localScale;
        Vector3 originalScale2 = pared2.transform.localScale;
        float elapsed = 0f;

        while (elapsed < shrinkDuration)
        {
            elapsed += Time.deltaTime;
            float scale = Mathf.Lerp(1, 0, elapsed / shrinkDuration);
            pared.transform.localScale = new Vector3(originalScale.x * scale, originalScale.y * scale, originalScale.z * scale);
            pared2.transform.localScale = new Vector3(originalScale2.x * scale, originalScale2.y * scale, originalScale2.z * scale);
            yield return null; // Esperar al siguiente frame
        }

        pared.SetActive(false);
        pared2.SetActive(false);
        isShrinking = false;
    }




}
