using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProyectileDamage : MonoBehaviour
{
    public float damage;
    public float pushBackForce;
    public float horizontalStartingLift;

    bool playerInRange = false;

    GameObject thePlayer;
    playerHealth thePlayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        thePlayerHealth = thePlayer.GetComponent<playerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            Attack();
            Destroy(gameObject.transform.root.gameObject);
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
        
        thePlayerHealth.addDamage(damage);
        pushBack(thePlayer.transform);
        
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
