using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVehicleController : MonoBehaviour
{
    public float speed = 5f; // Speed of the vehicle
    public Waypoint currentWaypoint; // Current waypoint the vehicle is heading towards

    void Start()
    {
        // Start moving towards the closest waypoint
        MoveTowardsClosestWaypoint();
    }

    void Update()
    {
        // If the vehicle has reached the current waypoint, move towards the next one
        if (currentWaypoint != null && Vector3.Distance(transform.position, currentWaypoint.transform.position) < 0.1f)
        {
            // Remove the current waypoint
            Destroy(currentWaypoint.gameObject);

            // Move towards the closest waypoint
            MoveTowardsClosestWaypoint();
        }
    }

    void MoveTowardsClosestWaypoint()
    {
        // Find the closest waypoint
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        Waypoint closestWaypoint = null;
        float closestDistance = Mathf.Infinity;

        foreach (Waypoint waypoint in waypoints)
        {
            float distance = Vector3.Distance(transform.position, waypoint.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestWaypoint = waypoint;
            }
        }

        // Move towards the closest waypoint if one is found
        if (closestWaypoint != null)
        {
            currentWaypoint = closestWaypoint;
            MoveTowardsWaypoint(currentWaypoint);
        }
        else
        {
            Debug.LogError("No waypoints found.");
            enabled = false; // Disable the script if no waypoints are found
        }
    }

    void MoveTowardsWaypoint(Waypoint waypoint)
    {
        // Calculate direction towards the waypoint
        Vector3 direction = waypoint.transform.position - transform.position;
        direction.y = 0f; // Ignore vertical component

        // Rotate towards the waypoint
        transform.rotation = Quaternion.LookRotation(direction);

        // Move forward towards the waypoint
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

