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

    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = waypoints4.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        //Sets the next waypoint target
        currentWaypoint = waypoints4.GetNextWaypoint(currentWaypoint);

        //rotates the vehicles
        transform.LookAt(currentWaypoint);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = waypoints4.GetNextWaypoint(currentWaypoint);
            transform.LookAt(currentWaypoint);
        }
    }
}