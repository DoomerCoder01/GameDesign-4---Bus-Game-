using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusPickup : MonoBehaviour
{

    private int passengersPickedUp = 0;
    public PassengerSpawner passengerSpawner;
    public Transform dropOffPoint;
    public GameObject miniMapIcon;
    private bool dropOffActive = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PassengerTrigger"))
        {
            PickupPassenger(other.transform.parent.gameObject);
        }
    }

    void PickupPassenger(GameObject passenger)
    {
        passenger.SetActive(false); // Deactivate the passenger
        passengersPickedUp++;

        if (passengersPickedUp == passengerSpawner.numberOfPassengers)
        {
            ActivateDropOffPoint();
        }
    }

    void ActivateDropOffPoint()
    {
        dropOffActive = true;
        miniMapIcon.SetActive(true); // Assuming miniMapIcon represents the drop-off point on the mini-map
    }

    void Update()
    {
        if (dropOffActive && Vector3.Distance(transform.position, dropOffPoint.position) < 1f)
        {
            DropOffPassengers();
        }
    }

    void DropOffPassengers()
    {
        passengersPickedUp = 0;
        // Implement payment logic here
        Debug.Log("Passengers dropped off. Payment processed.");
        dropOffActive = false;
        miniMapIcon.SetActive(false);
    }
   
}
