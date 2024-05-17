using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootEnemyProyectile : MonoBehaviour{

    public float damage;
    public float speed;

    Rigidbody myRB;

    GameObject thePlayer;
    playerHealth thePlayerHealth;

    // Start is called before the first frame update
    void Start(){
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        thePlayerHealth = thePlayer.GetComponent<playerHealth>();

        myRB = GetComponentInParent<Rigidbody>();
        if (transform.rotation.y>0){
            myRB.AddForce(Vector3.right*speed, ForceMode.Impulse);
        }
        else {
            myRB.AddForce(Vector3.right * -speed, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update(){
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("Shootable")){
            myRB.velocity = Vector3.zero;
            if (other.tag == "Player")
            {
                thePlayerHealth.addDamage(damage);
            }
            

            Destroy(gameObject);

        }
    }
}
