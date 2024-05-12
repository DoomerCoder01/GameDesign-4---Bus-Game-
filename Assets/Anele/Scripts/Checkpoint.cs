using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public PassengerSpawner passengerSpawner;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bus"))
        {
            passengerSpawner.SpawnPassenger();
        }
    }
}
