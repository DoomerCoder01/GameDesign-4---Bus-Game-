using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSpawner : MonoBehaviour
{

    public GameObject passengerPrefab;
    public List<Transform> spawnPoints;
    public int numberOfPassengers = 9;
    private List<GameObject> spawnedPassengers = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        SpawnPassengers();
    }

    void SpawnPassengers()
    {
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
