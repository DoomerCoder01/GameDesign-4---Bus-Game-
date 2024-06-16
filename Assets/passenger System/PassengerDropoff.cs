using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerDropoff : MonoBehaviour
{
    private bool isInsideTrigger = false;
    private bool hasSpawnedPassenger = false;
    public float timeInsideTrigger;
    public int currencyReward = 100; // The reward for dropping off a passenger
    private PassengerManager passengerManager; // Reference to PassengerManager
    Level1Manager level1;

    void Start()
    {
        // Find and reference the PassengerManager
        passengerManager = FindObjectOfType<PassengerManager>();
        level1 = FindObjectOfType<Level1Manager>();
    }

    void Update()
    {
        if (isInsideTrigger && PassengerController.passengerCount >= 1 && !hasSpawnedPassenger)
        {
            timeInsideTrigger += Time.deltaTime;
            if (timeInsideTrigger >= 3f)
            {
                // Deduct the passenger count
                PassengerController.passengerCount--;

                if (level1 != null)
                {
                    level1.passengerCount++;
                }
                // Reward the player with currency
                PassengerController.AddCurrency(currencyReward);
                Debug.Log("Player received " + currencyReward + " currency. Total currency: " + PassengerController.currency);

                // Check if passengerManager is not null before calling HandlePassengerDropoff
                if (passengerManager != null)
                {
                    passengerManager.HandlePassengerDropoff();
                }
                else
                {
                    Debug.LogError("PassengerManager not found!");
                }

                // Set hasSpawnedPassenger to true
                hasSpawnedPassenger = true;
                // Reset the timeInsideTrigger
                timeInsideTrigger = 0f;
            }
        }

        Debug.Log(gameObject.name);
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
