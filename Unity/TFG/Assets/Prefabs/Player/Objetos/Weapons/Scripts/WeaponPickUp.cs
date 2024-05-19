using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public int whichWeapon;
    public AudioClip pickUpClip;

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
            other.gameObject.GetComponent<playerInventoryManager>().activateWeapon(whichWeapon);
            //Destroy(transform.root.gameObject);
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(pickUpClip, transform.position);
        }
    }

}
