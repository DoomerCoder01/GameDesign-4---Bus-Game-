using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
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
