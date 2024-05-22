using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wAyPointMaker : MonoBehaviour
{
    //Stores a reference to the waypoint system the bus will use
    [SerializeField] private WaypointS waypoints4;

    [SerializeField] private float distanceThreshold = 0.1f;

    public float moveSpeed = 5f;

    private Transform currentWaypoint;

    [SerializeField] private Transform startingWaypoint;


    // Start is called before the first frame update
    void Start()
    {
        // If a starting waypoint is set, use it, otherwise get the first waypoint
        currentWaypoint = startingWaypoint != null ? startingWaypoint : waypoints4.GetNextWaypoint(null);

        // Set the initial position and orientation of the AI vehicle
        transform.position = currentWaypoint.position;
        currentWaypoint = waypoints4.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

        // Check if the vehicle is close enough to the current waypoint to switch to the next one
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = waypoints4.GetNextWaypoint(currentWaypoint);
            transform.LookAt(currentWaypoint);
        }
    }
}
