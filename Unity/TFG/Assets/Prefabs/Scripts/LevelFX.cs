using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFX : MonoBehaviour
{
    public static LevelFX instanciate;

    public AudioSource playerAS;
    public AudioClip deathMenuSong;


    void Awake()
    {
        instanciate = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playMenuMusic(AudioClip song)
    {
        
        playerAS.clip = song;
        playerAS.Play();
    }
}
