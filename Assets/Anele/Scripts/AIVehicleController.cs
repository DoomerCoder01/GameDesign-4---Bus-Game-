using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVehicleController : MonoBehaviour
{

    public float speed = 305f;
    public Waypoint currentWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        if (currentWaypoint != null)
            MoveTowardsWaypoint(currentWaypoint);
        else
            Debug.LogError("No initial waypoint assigned to AIVehicleController.");
    }

    // Update is called once per frame
    void Update()
    {
        // If the vehicle has reached the current waypoint, move towards the next one
        if (currentWaypoint != null && Vector3.Distance(transform.position, currentWaypoint.transform.position) < 0.1f)
        {
            // Get the next waypoint in the sequence
            Transform nextWaypointTransform = currentWaypoint.GetNextWaypoint(transform);

            // If there's a valid next waypoint, move towards it
            if (nextWaypointTransform != null)
            {
                currentWaypoint = nextWaypointTransform.GetComponent<Waypoint>();
                MoveTowardsWaypoint(currentWaypoint);
            }
            else
            {
                Debug.Log("No next waypoint found. Vehicle stopped.");
                enabled = false; // Disable the script if there's no next waypoint
            }
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
