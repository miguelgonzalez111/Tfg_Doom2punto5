using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class BossAnim : MonoBehaviour
{
    public float timeBetweenAttacks;
    float nextAttack;
    public int AttackNumber;
    public AudioClip[] AtackSounds;
    public AudioClip[] IdleSounds;
    public float IdleSoundTime;
    Animator animator;
    bool playerIn;

    AudioSource enemyAS;
    bool IsLock;

    GameObject healthBar;

    float nextIdleSound ;
    // Start is called before the first frame update
    void Start()
    {
        enemyAS = GetComponentInParent<AudioSource>();
        healthBar = GetComponentInChildren<EnemyHealth>().healthBar;
        nextAttack = Time.time;
        animator = GetComponent<Animator>();
        IsLock = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(1).IsTag("Attack"))
        {
            IsLock = false;
            
        }
        else
        {
            IsLock = true;
            if (Random.Range(0, 10) > 5 && nextIdleSound < Time.time && playerIn)
            {
                AudioClip tempClip = IdleSounds[Random.Range(0, IdleSounds.Length)];
                enemyAS.clip = tempClip;
                enemyAS.Play();
                nextIdleSound = IdleSoundTime + Time.time;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && IsLock)
        {
            healthBar.gameObject.SetActive(true);
            IsLock = true;
            playerIn = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player") && IsLock)
        {
            Vector3 directionToPlayer = other.transform.position - transform.position;
            // Mantener solo la rotación en el plano horizontal (Y)
            directionToPlayer.y = 0f;
            // Rotar para mirar hacia el jugador
            transform.rotation = Quaternion.LookRotation(directionToPlayer);


            
        }        

        if (other.CompareTag("Player") && nextAttack < Time.time && !(animator.GetCurrentAnimatorStateInfo(1).IsTag("Attack")))
        {
            nextAttack = Time.time + timeBetweenAttacks;

            int numeroAleatorio = Random.Range(0, AttackNumber);

            AudioClip tempClip = AtackSounds[numeroAleatorio];
            enemyAS.PlayOneShot(tempClip);

            
            animator.SetInteger("AttackNum", numeroAleatorio);
            animator.SetTrigger("AttackTrigger");

        }

    }
}
