using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerManager : MonoBehaviour
{
    public GameObject passengerPrefab; // Assign your passenger prefab in the Inspector
    public Transform[] spawnPoints; // Array of spawn points
    [SerializeField] private int passengersDroppedOff = 0; // Counter for dropped-off passengers



    // Method to spawn two passengers at random spawn points
    public void SpawnPassengersAtRandomPoints()
    {
        if (spawnPoints.Length < 2)
        {
            Debug.LogWarning("Not enough spawn points to spawn two passengers.");
            return;
        }

        // Generate two unique random indices
        int randomIndex1 = Random.Range(0, spawnPoints.Length);
        int randomIndex2;
        do
        {
            randomIndex2 = Random.Range(0, spawnPoints.Length);
        } while (randomIndex2 == randomIndex1);

        // Spawn passengers at the selected spawn points
        GameObject passenger1 = Instantiate(passengerPrefab, spawnPoints[randomIndex1].position, spawnPoints[randomIndex1].rotation);
        GameObject passenger2 = Instantiate(passengerPrefab, spawnPoints[randomIndex2].position, spawnPoints[randomIndex2].rotation);

        // Activate children if needed
        ActivateAllChildren(passenger1);
        SetWaypoint1(passenger1);
        ActivateAllChildren(passenger2);
        SetWaypoint2(passenger2);
    }

    // Method to handle passenger drop-off
    public void HandlePassengerDropoff()
    {
        passengersDroppedOff++;
        if (passengersDroppedOff >= 2)
        {
            SpawnPassengersAtRandomPoints();
            passengersDroppedOff = 0; // Reset the counter
        }
    }

    void ActivateAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    void SetWaypoint1(GameObject gameObject)
    {
                 GameObject.Find("Passenger 1 Waypoint").GetComponent<MissionWaypoint>().target = gameObject.transform;
    }

    void SetWaypoint2(GameObject gameObject)
    {
        GameObject.Find("Passenger 2 Waypoint").GetComponent<MissionWaypoint>().target = gameObject.transform;
    }
}