using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BusMovement1 : MonoBehaviour
{
    [SerializeField] Petrol petrol;

    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;
    public Rigidbody rb;
    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle, maxSpeed;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        LogSpeed();
    }

    private void GetInput()
    {
        if (petrol.petrolAmount <= 0)
            return;
        else
        {
            // Steering Input
            horizontalInput = Gamepad.current.leftStick.x.ReadValue();

            // Acceleration and Reverse Input
            verticalInput = Gamepad.current.rightTrigger.ReadValue() - Gamepad.current.leftTrigger.ReadValue();

            // Breaking Input
            isBreaking = Gamepad.current.buttonSouth.isPressed;
        }

    }

    private void HandleMotor()
    {
        if (Mathf.Abs(rb.velocity.magnitude) < maxSpeed || (verticalInput < 0 && rb.velocity.z > 0) || (verticalInput > 0 && rb.velocity.z < 0))
        {
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;
            rb.drag = 0;  // Reset the drag when the bus is moving
        }
        else
        {
            frontLeftWheelCollider.motorTorque = 0;
            frontRightWheelCollider.motorTorque = 0;
            rb.drag = 3;  // Increase the drag when no inputs are being pressed
        }

        if (isBreaking)
        {
            if (rb.velocity.magnitude > 0.1f && verticalInput >= 0)
            {
                // Apply brake force
                currentbreakForce = breakForce;
            }
            else
            {
                // Apply reverse force
                frontLeftWheelCollider.motorTorque = -1 * motorForce;
                frontRightWheelCollider.motorTorque = -1 * motorForce;
                currentbreakForce = 0f;
            }
        }
        else
        {
            currentbreakForce = 0f;
        }

        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }


    private void LogSpeed()
    {
        float speed = rb.velocity.magnitude;
        Debug.Log("Current Speed: " + speed);
    }
}
