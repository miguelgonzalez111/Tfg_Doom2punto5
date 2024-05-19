using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent nmAgent;
    public GameObject player;
    public bool enemyMove;
    public bool findPlayer;
    public AudioClip[] AtackSounds;
    public AudioClip[] IdleSounds;
    public float IdleSoundTime;
    float nextIdleSound;
    AudioSource enemyAS;
    Animator animator;
    [Header("opciones")]
    public bool lockTarget=false;
    // Start is called before the first frame update
    void Start()
    {
        nmAgent = GetComponentInParent<NavMeshAgent>();
        enemyAS = GetComponentInParent<AudioSource>();
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(findPlayer && lockTarget) { 
            nmAgent.destination = player.transform.position;
        }

        if ( nextIdleSound < Time.time && findPlayer)
        {
            AudioClip tempClip = IdleSounds[Random.Range(0, IdleSounds.Length)];
            enemyAS.clip = tempClip;
            enemyAS.Play();
            nextIdleSound = IdleSoundTime + Time.time;
        }
        if (animator.GetCurrentAnimatorStateInfo(1).IsTag("Attack") && !enemyAS.isPlaying)
        {
            AudioClip tempClip = AtackSounds[Random.Range(0, AtackSounds.Length)];
            enemyAS.PlayOneShot(tempClip);
        }

    }
    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (lockTarget)
            {
                findPlayer = true;
                enemyMove = true;

            }
            else
            {
                nmAgent.destination = player.transform.position;
                nmAgent.isStopped = false;
                enemyMove = true;
            }
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !lockTarget)
        {
            nmAgent.isStopped = true;
            enemyMove = false;
        }
    }

}
