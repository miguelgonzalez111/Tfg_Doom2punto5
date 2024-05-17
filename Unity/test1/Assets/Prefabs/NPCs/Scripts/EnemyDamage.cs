using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage;
    public float damageRate;
    public float pushBackForce;
    public float horizontalStartingLift;

    float nextDamege;

    bool playerInRange = false;

    GameObject thePlayer;
    playerHealth thePlayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        nextDamege = Time.time;
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        thePlayerHealth = thePlayer.GetComponent<playerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            Attack();
        }
        if (ControlledCapsuleCollider.instance.IsGrounded())
        {
            Rigidbody pushedRB = thePlayer.transform.GetComponent<Rigidbody>();
            pushedRB.velocity = Vector3.zero;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //if (ControlledCapsuleCollider.instance.IsGrounded())
            //{
            //    thePlayer.transform.position = new Vector3(transform.position.x,transform.position.y+0.01f,transform.position.z);
            //}
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }
    void Attack()
    {
        if (nextDamege <= Time.time)
        {
            thePlayerHealth.addDamage(damage);
            nextDamege = Time.time + damageRate;
            pushBack(thePlayer.transform);
        }
    }
    void pushBack(Transform pushedObject)
    {

        float verticalStartingPush = ControlledCapsuleCollider.instance.IsGrounded() ? horizontalStartingLift : 0;

        ControlledCapsuleCollider.instance.SetVelocity(new Vector2(0, verticalStartingPush));

        Vector3 pushDirection = new Vector3((pushedObject.position.x - transform.position.x), (pushedObject.position.y - transform.position.y), 0).normalized;
        pushDirection *= pushBackForce;

        Rigidbody pushedRB = pushedObject.GetComponent<Rigidbody>();
        pushedRB.velocity = Vector3.zero;
        pushedRB.AddForce(pushDirection, ForceMode.Impulse);
    }
}
