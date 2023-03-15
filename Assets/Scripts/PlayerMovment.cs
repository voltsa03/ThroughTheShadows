using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{

    private Rigidbody rb;

    [SerializeField]
    private int rayLayer = 3;
    [SerializeField]
    private int maxSpeed, speedMultipler, curspeedX;
    



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        jumping();

        movement();
    }



    void movement()
    {
        Vector3 horizontal = transform.TransformDirection(Vector3.right);

        if (Input.GetKey(KeyCode.D) && (curspeedX < maxSpeed))
            curspeedX += speedMultipler;
        
           // curspeedX -= (speedMultipler / 2);
    }
    void jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1))// shoots a ray cast bellow the character
            {
                Debug.Log("raycast hit");
            }
        }

    }
}
