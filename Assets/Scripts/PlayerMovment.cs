using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
 
    [SerializeField]
    private int maxSpeed, speedMultipler, jumpMultiplyer, fallSpeed;

    [SerializeField]
    private float speedX;

    [SerializeField]
    private float stoppingSpeed;



    private float rayDistance, sideRayDistance;

    private bool grounded = false, notFalling = true;
    private Rigidbody rb;

    private Vector2 velocitySpeed;

    int groundLayerMask = 1 << 3;
    


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

        rayCasting();

        debuging();
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

        velocitySpeed = horizontal * speedX;
    }

    void jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
          
                rb.velocity = new Vector2(rb.velocity.x, jumpMultiplyer);
        }
    }
           

    void rayCasting()
    {
        RaycastHit hitDown;
        if (Physics.Raycast(transform.position, Vector3.down, out hitDown, 1, groundLayerMask))// shoots a ray cast bellow the character
        {
            rayDistance = hitDown.distance;
            if (rayDistance < 0.51)//_____________ set character hight__________________________________
                grounded = true;
            else
                grounded = false;
        }
      

        RaycastHit hitSide;
        if (Physics.Raycast(transform.position, Vector3.right, out hitSide, 1) || Physics.Raycast(transform.position, Vector3.left, out hitSide, 1))
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
        if(speedX != 0 && grounded)
        {
            if (speedX > 0)
                speedX -= stoppingSpeed;
            else if (speedX < 0)
                speedX += stoppingSpeed;
            else
                speedX = 0;
        }

        /*if (speedX == 30 && grounded == false)
        {
            Debug.Log("falling is progress");
            rb.velocity = new Vector2(rb.velocity.x, -fallSpeed);
        }*/
    }
      void debuging()
    {
        Debug.Log(notFalling + " not falling");
        
        Debug.Log(grounded + " grounded");
  

    }
}
