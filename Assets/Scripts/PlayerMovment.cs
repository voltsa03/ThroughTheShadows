using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
 
    [SerializeField]
    private int maxSpeed, jumpMultiplyer, secondJumpMultiplyer;

    [SerializeField]
    private float speedX;

    [SerializeField]
    private float speedMultipler, stoppingSpeed;

    public int dashingSpeed;

    private float rayDistance, sideRayDistance;

    private bool grounded = false, notFalling = true, doubleJump = false, isDashing = false;
    private Rigidbody rb;

    private Vector2 velocitySpeed;

    int groundLayerMask = 1 << 3;

    public Transform raycastPoint1, raycastPoint2, raycastPoint3, raycastPoint4;


    public GameObject playerObj;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
       
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(velocitySpeed.x, rb.velocity.y);
    }

    void Update()
    {
        //Vector3 down = transform.TransformDirection(Vector3.down) / 2;
        //Debug.DrawRay(transform.position, down, Color.red);
        movement();

        jumping();

        shrinking();

        rayCasting();

        //debuging();
        dashing();


        
    }

    void movement()
    {
        Vector2 horizontal = transform.TransformDirection(Vector2.right);
      
        if (Input.GetKey(KeyCode.D) && (speedX < maxSpeed) && grounded && notFalling)
            speedX += speedMultipler;
        
           // curspeedX -= (speedMultipler / 2);
        
        else if (Input.GetKey(KeyCode.A) && (speedX > -maxSpeed) && grounded && notFalling)
            speedX -= speedMultipler;

        else 
            stopping();

        //Mathf.RoundToInt(speedX);
        velocitySpeed = horizontal * Mathf.RoundToInt(speedX);
    }

    void jumping()
    {
       
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            StartCoroutine(doubleJumpDelay());
            rb.velocity = new Vector2(rb.velocity.x, jumpMultiplyer);
            
        }
        if (Input.GetKeyDown(KeyCode.Space) && doubleJump)
        {
           //Debug.Log("double jumping");
            rb.velocity = new Vector2(rb.velocity.x, secondJumpMultiplyer);
            doubleJump = false;
        }
       // if (Input.GetKeyDown(KeyCode.Space))
          //  rb.velocity = new Vector2(rb.velocity.x, jumpMultiplyer);
    }

    IEnumerator doubleJumpDelay()
    {
        yield return new WaitForSeconds(0.2f);
        doubleJump = true;
    }



    void shrinking()
    {//__________________________________________working on needs finishing 
        int numSwitch = 0;
        if (Input.GetKeyDown(KeyCode.Tab))
            ++numSwitch;
        
        //Debug.Log(numSwitch);
        switch (numSwitch)
        {
            case 1:
                playerObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case 2:
                playerObj.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
        }
        if (numSwitch >= 3)
            numSwitch = 0;
        //________________________________________________________________
    }


    void dashing()
    {
        float speedXValueHold = 0;
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
            StartCoroutine(dashDelayRight(speedXValueHold));

        else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
            StartCoroutine(dashDelayLeft(speedXValueHold));
           
    }

    IEnumerator dashDelayRight(float speedXValueHold) 
    {
        speedXValueHold = speedX;
        isDashing = true;
        speedX = dashingSpeed;
        yield return new WaitForSeconds(0.2f);
        speedX = speedXValueHold;
        isDashing = false;
    }

    IEnumerator dashDelayLeft(float speedXValueHold) 
    {
        speedXValueHold = speedX;
        isDashing = true;
        speedX = -dashingSpeed;
        yield return new WaitForSeconds(0.2f);
        speedX = speedXValueHold;
        isDashing = false;    

    }

    void rayCasting()
    {
        RaycastHit hitDown;
        if (Physics.Raycast(raycastPoint3.position, Vector3.down, out hitDown, 1, groundLayerMask) ||
            Physics.Raycast(raycastPoint4.position, Vector3.down, out hitDown, 1, groundLayerMask))// shoots a ray cast bellow the character
        {
            rayDistance = hitDown.distance;
            if (rayDistance < 0.51)//_____________ set character hight__________________________________
            {
                grounded = true;
                doubleJump = false;

            }
           
            else
                grounded = false;


        }
      

        RaycastHit hitSide;
        if (Physics.Raycast(raycastPoint1.position, Vector3.right, out hitSide, 1) || Physics.Raycast(raycastPoint2.position, Vector3.right, out hitSide, 1) || 
            Physics.Raycast(raycastPoint1.position, Vector3.left, out hitSide, 1) || Physics.Raycast(raycastPoint2.position, Vector3.left, out hitSide, 1))
        {
            sideRayDistance = hitSide.distance;
            if (sideRayDistance < 0.51 && grounded == false)// _______________set character thickness__________________________
            {
                speedX = 0;
                notFalling = false;
            }
            else
                notFalling = true;
                
        }
    }

    void stopping()
    {
        if(speedX != 0 && grounded && !isDashing)
        {
            if (speedX > 0)
                speedX -= stoppingSpeed;
            else if (speedX < 0)
                speedX += stoppingSpeed;
            else
                speedX = 0;
        }
    }

      void debuging()
      {
        Debug.Log(notFalling + " not falling");
        
        Debug.Log(grounded + " grounded");
      }
}
