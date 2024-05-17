using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpController : MonoBehaviour
{
    public GameObject FlipModel;
    public GameObject Proyectil;

    public float timeBetweenBullets;//Velocidad a la que disparas
    public float nextBullet;//Para que el codigo sepa cuando sale el siguiente proyectil
    public float startBullet;//El tiempo minimo que tardas en disparar el primer proyectil(No tocar)

    public bool InRange;
    //audio options
    public AudioClip[] IdleSounds;
    public float IdleSoundTime;
    AudioSource EnemyMovementAS;
    float nextIdleSound = 0;

    public float detectionTime;
    float startRun;
    bool firstDetection;

    //movement options
    public float runSpeed;
    public float walkSpeed;
    public bool facingRight = true;
    float moveSpeed;
    bool running;

    Rigidbody myRB;
    Animator myAnim;
    Transform detectedPlayer;
    bool Detected;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponentInParent<Rigidbody>();
        myAnim = GetComponentInParent<Animator>();
        EnemyMovementAS=GetComponent<AudioSource>();
        running = false;
        Detected = false;
        firstDetection = false;
        moveSpeed = walkSpeed;

        if (Random.Range(0, 10) > 5)
        {
            Flip();
        }

    }

    void Awake()
    {
        nextBullet = Time.time + startBullet; 
    }

        void FixedUpdate()
    {
        if(Detected)
        {
            if (detectedPlayer.position.x < transform.position.x && facingRight)
            {
                Flip();
            }
            else if (detectedPlayer.position.x > transform.position.x && !facingRight)
            {
                Flip();
            }
            if(!firstDetection)
            {
                startRun = Time.time+detectionTime;
                firstDetection=true;
            }
        }
        if(Detected && !facingRight)
        {
            myRB.velocity = new Vector3((moveSpeed * -1),myRB.velocity.y,0);

        }
        else if(Detected && facingRight) 
        {
            myRB.velocity = new Vector3(moveSpeed , myRB.velocity.y, 0);
        }
        if(!running && Detected)
        {
            if (startRun < Time.time)
            {
                moveSpeed = runSpeed;
                myAnim.SetTrigger("run");
                running = true;
            }
        }
        //idle walking sounds
        if(!running)
        {
            if(Random.Range(0, 10)>5 && nextIdleSound < Time.time)
            {
                AudioClip tempClip = IdleSounds[Random.Range(0, IdleSounds.Length)];
                EnemyMovementAS.clip = tempClip;
                EnemyMovementAS.Play();
                nextIdleSound=IdleSoundTime+Time.time;
            }
        }
        //Disaparar proyectil
        if(nextBullet < Time.time && InRange)
        {
            nextBullet = Time.time + timeBetweenBullets;
            Vector3 rot;//Vector para la direccion en la que se crea mirando el proyectil
            if (facingRight)
            {//Con el playerController comprobamos en que direccion estamos mirando 1==DERECHA -1==IZQUIERDA
                rot = new Vector3(0, 90, 0);
            }
            else
            {
                rot = new Vector3(0, -90, 0);
            }
            Instantiate(Proyectil, transform.position, Quaternion.Euler(rot));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player" && !Detected)
        {
            Detected = true;
            detectedPlayer = other.transform;
            myAnim.SetBool("detected",Detected);
            if(detectedPlayer.position.x < transform.position.x && facingRight)
            {
                Flip();
            }
            else if(detectedPlayer.position.x > transform.position.x && !facingRight)
            {
                Flip();
            }

            
        }
        if (other.tag == "Player")
        {
            InRange = true;
        }

        }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            InRange = false;
            firstDetection = false;
            if(running)
            {
                myAnim.SetTrigger("run");
                moveSpeed = walkSpeed;
                running = false;
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale=FlipModel.transform.localScale;
        theScale.z *= -1;
        FlipModel.transform.localScale = theScale;
    }
}
