using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fire : MonoBehaviour
{
    public float timeBetweenBullets;//Velocidad a la que disparas
    public GameObject projectile;//Lo que disparas

    float nextBullet;//Para que el codigo sepa cuando sale el siguiente proyectil
    public float startBullet;//El tiempo minimo que tardas en disparar el primer proyectil(No tocar)

    //Audio
    AudioSource gunMuzzleAS;//Donde suenan los audios
    public AudioClip shootSound;//Sonido al disparar
    public AudioClip pickWeapon;//Sonido al cambiar al arma
    //graphic info
    public Sprite weaponSprite;//El sprite que se pone en el hud
    public Image weaponImage;//En que parte del hud se pone

    void Awake()
    {
        nextBullet = Time.time + startBullet;//Seteamos el tiempo de el primer proyectil para controlar los demas
        gunMuzzleAS = GetComponent<AudioSource>();//Seteamos el lugar donde sonaran los audios
    }

    // Update is called once per frame
    void Update()
    {
        playerController myPlayer = transform.root.GetComponent<playerController>();//Obtenemos el controlador del jugador
        Animator animator = myPlayer.GetComponentInChildren<Animator>();

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetAxisRaw("Fire1") > 0 && nextBullet < Time.time && !(stateInfo.IsTag("NoShooting")))
        {//Miramos que se pulsa el boton de disparar y si el tiempo entre proyectiles es el adecuado continuamos
            nextBullet = Time.time + timeBetweenBullets;//Acctualizamos el tiempo para el siguiente proyectil
            Vector3 rot;//Vector para la direccion en la que se crea mirando el proyectil
            animator.SetTrigger("Shooting");
            if (myPlayer.getFacing() == -1f)
            {//Con el playerController comprobamos en que direccion estamos mirando 1==DERECHA -1==IZQUIERDA
                rot = new Vector3(0, -90, 0);
            }
            else
            {
                rot = new Vector3(0, 90, 0);
            }
            Instantiate(projectile, transform.position, Quaternion.Euler(rot));//Creamos el proyectil 

            AudioSource.PlayClipAtPoint(shootSound, transform.position);//Hacemos sonar el sonido de disparo en el mundo
        }
        if (Input.GetAxisRaw("Fire1") < 1 && nextBullet < Time.time)//Si dejas de pulsar el boton de dispara reseteamos la variable del siguiente proyectil
        {
            nextBullet = Time.time + startBullet;//Ponemos el proximo proyectil igual al primer proyectil
        }
    }

    public void initializeWeapon()//Este metodo se encarga de activar el arma cuando la seleccionas en tu inventario
    {
        gunMuzzleAS.clip = pickWeapon;//Seleccionamos el audio
        gunMuzzleAS.Play();//Hacemos sonar el audio de cambiar arma en el AudioSource 
        nextBullet = Time.time + startBullet;//Reiniciamos el tiempo del primer disparo
        weaponImage.sprite = weaponSprite;//Ponemos el sprite del arma en el hud
    }
}
