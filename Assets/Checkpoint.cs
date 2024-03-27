using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject PassengerPrefab;
    public int minPassengers = 1;
    public int maxPassengers = 5;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bus"))
        {
            SpawnPassengers();
        }
    }

    void SpawnPassengers()
    {
        int numPassengers = Random.Range(minPassengers, maxPassengers + 1);

        for (int i = 0; i < numPassengers; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Instantiate(PassengerPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
