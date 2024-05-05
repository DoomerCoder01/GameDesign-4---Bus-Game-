using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public List<Transform> waypoints;


    // Start is called before the first frame update
    void Start()
    {
        ConnectWaypoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ConnectWaypoints()
    {
        // Loop through each waypoint
        for (int i = 0; i < waypoints.Count; i++)
        {
            // Get the current waypoint
            Transform currentWaypoint = waypoints[i];

            // Get the next waypoint (loop back to the first waypoint if it's the last one)
            Transform nextWaypoint = waypoints[(i + 1) % waypoints.Count];

            // Connect the current waypoint to the next waypoint
            Connect(currentWaypoint, nextWaypoint);
        }
    }

    void Connect(Transform waypointA, Transform waypointB)
    {
        // Add waypointB to the list of connections for waypointA
        waypointA.GetComponent<Waypoint>().AddConnection(waypointB);
        // Uncomment the line below if you want bidirectional connections
        // waypointB.GetComponent<Waypoint>().AddConnection(waypointA);
    }


}
