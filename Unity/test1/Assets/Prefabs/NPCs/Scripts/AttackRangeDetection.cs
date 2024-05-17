using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeDetection : MonoBehaviour
{
    private Animator enemyAnim;
    float nextAttack;
    public float timeBetweenAttacks;

    private void Awake()
    {
        nextAttack = Time.time;
        enemyAnim = GetComponentInParent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && nextAttack < Time.time && !(enemyAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))) {
            nextAttack = Time.time + timeBetweenAttacks;
            enemyAnim.SetTrigger("Attack");
        }
    }
}
