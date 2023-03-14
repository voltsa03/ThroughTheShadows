using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayPlayer1 : MonoBehaviour
{
    public AudioClip footstep;
    public AudioSource audioSource;
    private Animator anim;
   
   

    public float audioVolume = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

   
    public void RunSound()
    {
        audioSource.clip = footstep;
        audioSource.volume = audioVolume;   
        audioSource.Play();
        Debug.Log("Running sound has been triggered");
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            Debug.Log("D or A input has been inputed");
            RunSound();
        }
    }



}
