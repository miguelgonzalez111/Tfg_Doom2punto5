using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    private Animator enemyAnim;
    public GameObject proyectil;
    float nextAttack;
    public float timeBetweenAttacks;
    public float timeBeforeAttack;

    public Transform startingPoint;
    

    private void Awake()
    {
        nextAttack = Time.time;
        enemyAnim = GetComponentInParent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && nextAttack < Time.time && !(enemyAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) )
        {

            nextAttack = Time.time + timeBetweenAttacks;
            enemyAnim.SetTrigger("RangedAttack");
            StartCoroutine(waitToAttack());
            
        }
    }
    IEnumerator waitToAttack()
    {
        yield return new WaitForSeconds(timeBeforeAttack);
        Instantiate(proyectil, startingPoint.position, Quaternion.identity);
    }
}
