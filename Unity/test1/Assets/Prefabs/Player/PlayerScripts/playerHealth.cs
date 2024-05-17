using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float currentHealth;
    public float fullHealth;
    public float lowHealth;

    public GameObject playerDeathFX;
    public GameObject playerDeathFX2;

    [Header("HUD")]
    public Slider playerHealthSlider;
    public Image damageScreen;
    Color flashColor = new Color(255f, 0f, 0f,1f);
    float flashSpeed = 5f;
    bool damaged = false;

    [Header("AudioSource")]
    AudioSource playerAS;
    public AudioClip[] damageSounds;
    public AudioClip[] lowHealthSounds;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = fullHealth;
        playerHealthSlider.maxValue = fullHealth;
        playerHealthSlider.value = fullHealth;
        playerAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //are we hurt
        if (damaged)
        {
            damageScreen.color = flashColor;
        }
        else
        {
            damageScreen.color = Color.Lerp(damageScreen.color,Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        if (currentHealth <= lowHealth && lowHealthSounds.Length > 0 && !playerAS.isPlaying)
        {
            playerAS.clip = lowHealthSounds[(int)Random.Range(0, lowHealthSounds.Length)];
            playerAS.Play();
            
        }
    }

    public void addDamage(float damage)
    {
        playerController myPlayer = transform.root.GetComponent<playerController>();//Obtenemos el controlador del jugador
        Animator animator = myPlayer.GetComponentInChildren<Animator>();
        animator.SetTrigger("Damage");
        currentHealth -= damage;
        playerHealthSlider.value = currentHealth;
        damaged = true;
        Instantiate(playerDeathFX, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        if(damageSounds.Length>0){
            playerAS.clip = damageSounds[(int)Random.Range(0, damageSounds.Length)];
            playerAS.Play();
        }
        
        if(currentHealth <= 0) {
            makeDead();
        }
    }
    public void addHealth(float health)
    {
        currentHealth += health;
        if(currentHealth > fullHealth) {
            currentHealth=fullHealth;
        }
        playerHealthSlider.value = currentHealth;
    }
    public void makeDead()
    {
        Instantiate(playerDeathFX2, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Instantiate(playerDeathFX,transform.position, Quaternion.Euler(new Vector3(0,0,0)));
        damageScreen.color = flashColor;
        gameObject.SetActive(false);
    }
}
