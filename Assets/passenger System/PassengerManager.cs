using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerManager : MonoBehaviour
{
    public GameObject passengerPrefab; // Assign your passenger prefab in the Inspector
    public Transform[] spawnPoints; // Array of spawn points
    private int passengersDroppedOff = 0; // Counter for dropped-off passengers

    // Method to spawn a passenger at a random spawn point
    public void SpawnPassenger()
    {
        if (spawnPoints.Length == 0) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];
        Instantiate(passengerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    // Method to handle passenger drop-off
    public void HandlePassengerDropoff()
    {
        passengersDroppedOff++;
        if (passengersDroppedOff >= 2)
        {
            SpawnPassenger();
            passengersDroppedOff = 0; // Reset the counter
        }
    }
}