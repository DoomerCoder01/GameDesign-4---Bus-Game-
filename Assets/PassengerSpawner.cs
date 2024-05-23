using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSpawner : MonoBehaviour
{
    public GameObject passengerPrefab; // Drag your Passenger prefab here in the Inspector
    public GameObject passengerZonePrefab; // Drag your Passenger Zone prefab here in the Inspector
    public int numberOfPassengers = 9;
    private List<GameObject> spawnedPassengers = new List<GameObject>();

    void Start()
    {
        SpawnPassengerZoneAndPassengers();
    }

    void SpawnPassengerZoneAndPassengers()
    {
        // Instantiate the Passenger Zone
        GameObject passengerZoneInstance = Instantiate(passengerZonePrefab, transform.position, transform.rotation);

        // Find all child transforms of the instantiated Passenger Zone and add them as spawn points
        List<Transform> spawnPoints = new List<Transform>();
        foreach (Transform child in passengerZoneInstance.transform)
        {
            spawnPoints.Add(child);
        }

        // Spawn passengers at the spawn points
        for (int i = 0; i < numberOfPassengers; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Count);
            Transform spawnPoint = spawnPoints[randomIndex];
            GameObject passenger = Instantiate(passengerPrefab, spawnPoint.position, spawnPoint.rotation);
            spawnedPassengers.Add(passenger);
        }
    }

    public List<GameObject> GetSpawnedPassengers()
    {
        return spawnedPassengers;
    }

}
