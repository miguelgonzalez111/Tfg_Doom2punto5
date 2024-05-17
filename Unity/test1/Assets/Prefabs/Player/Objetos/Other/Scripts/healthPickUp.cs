using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float healthAmount;
    public AudioClip healthPickUpSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<playerHealth>().addHealth(healthAmount);
            //Destroy(transform.root.gameObject);
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(healthPickUpSound, transform.position, 1f);
        }
    }
}
