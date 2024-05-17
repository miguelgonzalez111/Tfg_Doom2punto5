using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootSpread : MonoBehaviour{

    public float damage;
    public float speed;
    public float maxSpread;
    public float minSpread;
    Rigidbody myRB;

    // Start is called before the first frame update
    void Start(){
        //tocar eje x , z para mirar el spread
        Vector3 dir = transform.up + new Vector3(0, Random.Range(minSpread, maxSpread), Random.Range(-maxSpread, maxSpread));


        myRB = GetComponentInParent<Rigidbody>();
        if (transform.rotation.y>0){
            myRB.AddForce(dir * speed, ForceMode.Impulse);
            //myRB.AddForce(Vector3.right*speed, ForceMode.Impulse);
        }
        else {
            myRB.AddForce(dir * speed, ForceMode.Impulse);
            //myRB.AddForce(Vector3.right * -speed, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update(){
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" || other.gameObject.layer == LayerMask.NameToLayer("Shootable")){
            myRB.velocity = Vector3.zero;
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.addDamage(damage);
            }
            Destroy(gameObject);

        }
    }
}
