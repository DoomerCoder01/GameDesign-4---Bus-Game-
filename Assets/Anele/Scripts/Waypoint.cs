using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private List<Transform> connections = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
     public void AddConnection(Transform waypoint)
        {
            // Add the waypoint to the list of connections
            connections.Add(waypoint);
        }

    // Method to get the next waypoint in the sequence
    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        // If there are no connections, return null
        if (connections.Count == 0)
            return null;

        // Find the index of the current waypoint in the connections list
        int currentIndex = connections.IndexOf(currentWaypoint);

        // If the current waypoint is not found in the connections list, return null
        if (currentIndex == -1)
            return null;

        // Get the index of the next waypoint (loop back to the first waypoint if it's the last one)
        int nextIndex = (currentIndex + 1) % connections.Count;

        // Return the next waypoint
        return connections[nextIndex];
    }


}


