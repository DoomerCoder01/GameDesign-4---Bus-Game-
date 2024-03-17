using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusMovement : MonoBehaviour
{
    public float speed = 5f; 
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        
        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);

        rb.velocity = movement * speed;
    }

   
}
