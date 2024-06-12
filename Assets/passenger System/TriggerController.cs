using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public PassengerController passengerController; // Assign your PassengerController in the Inspector
    private Vector3 originalPosition; // To store the original position of the passenger

    [SerializeField] GameObject greenBox;

    void Start()
    {
        Invoke("StoreOriginalPosition", 0.5f); // Wait for half a second before storing the original position
    }

    void StoreOriginalPosition()
    {
        originalPosition = passengerController.passengerModel.transform.position; // Store the original position of the passenger
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player vehicle has entered this object's trigger
        if (other.gameObject == passengerController.playerVehicle)
        {
            passengerController.isMoving = true;
            passengerController.animator.SetBool("GetON", true); // Trigger the walking animation
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player vehicle has left the trigger object's trigger
        if (other.gameObject == passengerController.playerVehicle)
        {
            passengerController.isMoving = false;
            passengerController.animator.SetBool("GetON", false); // Stop the walking animation
            passengerController.passengerModel.transform.position = originalPosition; // Reset the passenger to their original position
            greenBox.SetActive(false);
        }
    }
}
