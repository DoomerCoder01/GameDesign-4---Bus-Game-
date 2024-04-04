using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    Win_Lose wL;

    public PassengerSpawner passengerSpawner;

    void Start()
    {
        wL = GameObject.Find("UI").GetComponent<Win_Lose>();
        passengerSpawner = GameObject.Find("CheckpointManager").GetComponent<PassengerSpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bus"))
        {
            passengerSpawner.SpawnPassenger();
            wL.passengers++;
            Destroy(gameObject);
        }
    }
}
