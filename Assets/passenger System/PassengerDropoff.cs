using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerDropoff : MonoBehaviour
{
    public GameObject passengerPrefab; // Assign your passenger prefab in the Inspector
    private bool isInsideTrigger = false;
    private bool hasSpawnedPassenger = false;
    public float timeInsideTrigger;

    void Update()
    {
        if (isInsideTrigger && PassengerController.passengerCount >= 1 && !hasSpawnedPassenger)
        {
            timeInsideTrigger += Time.deltaTime;
            if (timeInsideTrigger >= 3f)
            {
                // Deduct the passenger count
                PassengerController.passengerCount--;
                // Spawn a new passenger prefab within the trigger bounds
                GameObject newPassenger = Instantiate(passengerPrefab, transform.position, Quaternion.identity);
                // Destroy the new passenger after 6 seconds
                Destroy(newPassenger, 6f);
                // Set hasSpawnedPassenger to true
                hasSpawnedPassenger = true;
                // Reset the timeInsideTrigger
                timeInsideTrigger = 0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the vehicle has entered a trigger
        if (other.gameObject.CompareTag("DropOff"))
        {
            isInsideTrigger = true;
            hasSpawnedPassenger = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the vehicle has exited a trigger
        if (other.gameObject.CompareTag("DropOff"))
        {
            isInsideTrigger = false;
            timeInsideTrigger = 0f;
            hasSpawnedPassenger = false;
        }
    }
}
