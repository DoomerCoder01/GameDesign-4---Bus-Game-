using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusMovement : MonoBehaviour
{
    public float acceleration = 5f;
    public float maxSpeed = 10f;
    public float rotationSpeed = 100f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // This one calculates the acceleration Unathi
        Vector3 accelerationVector = transform.forward * verticalInput * acceleration;

        // This one applies the acceleration
        rb.AddForce(accelerationVector, ForceMode.Acceleration);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        float rotation = horizontalInput * rotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotation);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }



}
