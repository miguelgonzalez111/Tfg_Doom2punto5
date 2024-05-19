using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public float enemyMaxHealth;
    public float currentHealth;
    public float damageModifier;
    public GameObject damageParticles;//Sangre
    public GameObject deathParticles;//Muerte
    public GameObject drop;//Lo que dropean
    public bool drops;//Si dropean algo
    public AudioClip[] deathSounds;
    public AudioClip[] damageSounds;
    AudioSource enemyAS;

    public bool IsBoss;
    Collider[] colliders;
    public GameObject healthBar;
    Slider healthBarSlider;


    // Start is called before the first frame update
    void Start()
    {

        currentHealth = enemyMaxHealth;
        enemyAS = GetComponentInParent<AudioSource>();
        if (IsBoss)
        {
            colliders = GetComponentsInChildren<Collider>();
            healthBarSlider = healthBar.GetComponentInChildren<Slider>();
            healthBarSlider.maxValue = enemyMaxHealth;
            healthBarSlider.value = currentHealth;
            healthBar.gameObject.SetActive(false);
            // Iterar a través de los colliders encontrados
            foreach (Collider collider in colliders)
            {
                // Hacer algo con cada collider, como imprimir su nombre
                Debug.Log("Collider encontrado: " + collider.gameObject.name);
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addDamage(float damage)
    {
        Debug.Log("Soy un zombi dolorido: ");
        damage = damage * damageModifier;
        if (damage > 0f)
        {

            currentHealth -= damage;
            if (damageSounds != null)
            {
                playSound(damageSounds);
            }
            damageFX(damageParticles, transform.position, new Vector3(0, 0, 0));
            if (currentHealth <= 0f)
            {
                makeDead();
            }

            if (IsBoss) {
                healthBarSlider.value = currentHealth;
            }

        }
    }

    public void damageFX(GameObject particle, Vector3 point, Vector3 rotation)
    {
        Instantiate(particle, point, Quaternion.Euler(rotation));
    }
    public void playSound(AudioClip[] sounds)
    {
        AudioClip tempClip = sounds[Random.Range(0, sounds.Length)];
        enemyAS.PlayOneShot(tempClip);
        //enemyAS.clip = tempClip;
        //enemyAS.Play();
    }
    void makeDead()
    {
        //turn off movement
        //create ragdoll
        //playSound(deathSounds);
        if (IsBoss)
        {
            healthBar.gameObject.SetActive(false);
        }
        if (deathSounds != null)
        {
            AudioClip tempClip = deathSounds[Random.Range(0, deathSounds.Length)];
            AudioSource.PlayClipAtPoint(tempClip, transform.position);
        }

        damageFX(deathParticles, transform.position, new Vector3(0, 0, 0));
        if (drops)
        {
            Instantiate(drop, transform.position, drop.transform.rotation);
        }
        //Destroy(gameObject.transform.root.gameObject);
        //gameObject.SetActive(false);
        GameObject parentObject = gameObject.transform.parent.gameObject;
        parentObject.SetActive(false);
    }
}
