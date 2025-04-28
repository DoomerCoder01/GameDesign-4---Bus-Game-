using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bus"))
        {
            PassengersForest.Instance.PassengerCollected(gameObject);
        }
    }
}
