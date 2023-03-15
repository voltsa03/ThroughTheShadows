using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Public Variables")]

    public float speed;
    public float jump;
    private float moveVelocity;
    public float boostStrengh;
  

    public AudioClip footstep;
    
    public AudioSource audioSource;
    private Animator anim;

    private bool grounded = true;

    private Rigidbody rb;
    public float audioVolume = 0.25f;

    public float walkingtime;

    private bool walkbool = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

   /*public void OnCollisionEnter(Collision collisioninfo)
    {
        if (collisioninfo.gameObject.tag == "Ground")
        {
            grounded = true;
        }
        else
            grounded = false;

    }*/

    // Update is called once per frame
    void Update()
    {

      if (Input.GetKeyDown(KeyCode.Space))
        {
           // Debug.Log("keyboard input detected");
            if (grounded)
                Jump(true);
            else
                Jump(false);

        }
        if (Input.GetKey(KeyCode.A))
            Move(-speed);

        if (Input.GetKey(KeyCode.D))
            Move(speed);
        if (!Input.anyKey)
            moveVelocity = 0;

        if (Input.GetKey(KeyCode.A) && walkbool && grounded || Input.GetKey(KeyCode.D) && walkbool && grounded)
        {
           // StartCoroutine(walkingcycle(walkingtime));   Enable when sound is ready to be added
        }


        
    }
    
    public void Walking()
    {
        audioSource.clip = footstep;
        audioSource.volume = audioVolume;
        audioSource.Play();
    }


    
    

    IEnumerator walkingcycle(float walkingtime)
    {
        walkbool = false;
        Walking();
        //Debug.Log("walking Ienumerator");
        yield return new WaitForSeconds(walkingtime);
        walkbool = true;
    }
    
    public void FixedUpdate()
    {
        rb.velocity = new Vector2(moveVelocity, rb.velocity.y);

    }
    public void Jump(bool isGrounded)
    {
        if (isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jump);
        else if (!isGrounded)
        {

        }
    }
    public void Move(float amount)
    {
        moveVelocity = amount;
    }
   private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("Boost");
        grounded = true;
        rb.velocity = transform.up * boostStrengh;
        if (other.gameObject.tag == "Boost")
        {
            rb.AddForce(transform.up * boostStrengh);
            Debug.Log("Boost");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        grounded = false;
    }
}

