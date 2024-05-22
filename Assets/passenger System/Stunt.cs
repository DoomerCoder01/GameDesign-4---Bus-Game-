using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunt : MonoBehaviour
{
    public WheelCollider[] wheels; // Assign your Wheel Colliders here
    public LayerMask groundLayer; // Assign the layer of the ground here
    private float airtime = 0f;
    private int score = 0;
    private bool isInAir = false;

    void Update()
    {
        // Check if bus is in the air
        bool inAir = CheckIfInAir();
        if (inAir)
        {
            // Increase airtime
            airtime += Time.deltaTime;
        }
        else if (isInAir)
        {
            // Bus has landed, convert airtime to points
            int points = AirtimeToPoints(airtime);
            score += points;
            // Print airtime and score gain to console
            Debug.Log("Airtime: " + airtime + " seconds");
            Debug.Log("Score Gain: " + points);
            // Reset airtime
            airtime = 0f;
        }
        isInAir = inAir;
    }

    bool CheckIfInAir()
    {
        foreach (WheelCollider wheel in wheels)
        {
            // Send a raycast down from the wheel and check if it hits the ground
            if (Physics.Raycast(wheel.transform.position, -wheel.transform.up, out RaycastHit hit, wheel.radius + 0.1f, groundLayer))
            {
                // If any wheel is touching the ground, return false
                return false;
            }
        }
        // If none of the wheels are touching the ground, return true
        return true;
    }

    int AirtimeToPoints(float airtime)
    {
        // Define conversion rate from airtime to points
        int conversionRate = 10;
        return Mathf.FloorToInt(airtime * conversionRate);
    }


}
