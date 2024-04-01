using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSpawner : MonoBehaviour
{
    public GameObject PassengerPrefab;
    public Transform[] spawnPoints; // Array to store spawn points
    private bool[] spawnPointUsed;  // Array to track if a spawn point has been used
    private int numPassengersSpawned; // Number of passengers spawned

    void Start()
    {
        InitializeSpawnPoints();
        SpawnPassenger();
    }

    void InitializeSpawnPoints()
    {
        // Initialize the array of spawn points and spawn point usage
        spawnPointUsed = new bool[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPointUsed[i] = false;
        }
    }

    public void SpawnPassenger()
    {
        // Check if all spawn points have been used
        if (numPassengersSpawned >= spawnPoints.Length)
        {
            // Game is done, no more passengers to spawn
            return;
        }

        // Find an unused spawn point
        int spawnIndex = FindUnusedSpawnPoint();
        if (spawnIndex == -1)
        {
            // No unused spawn points found, something went wrong
            Debug.LogError("No unused spawn points found!");
            return;
        }

        // Spawn a passenger at the selected spawn point
        Instantiate(PassengerPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);

        // Mark the spawn point as used
        spawnPointUsed[spawnIndex] = true;

        // Increment the number of passengers spawned
        numPassengersSpawned++;
    }

    int FindUnusedSpawnPoint()
    {
        // Find an unused spawn point and return its index
        for (int i = 0; i < spawnPointUsed.Length; i++)
        {
            if (!spawnPointUsed[i])
            {
                return i;
            }
        }
        // If all spawn points have been used, return -1
        return -1;
    }
}
