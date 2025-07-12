using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerCollect : MonoBehaviour
{
    public GameObject passenger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bus"))
        {
            PassengersForest.Instance.PassengerCollected(gameObject);
            Destroy(passenger);
        }
    }
}
