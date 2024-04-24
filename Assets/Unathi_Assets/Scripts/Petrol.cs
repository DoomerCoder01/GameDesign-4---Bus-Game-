using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Petrol : MonoBehaviour
{
    public Slider slider;
    public float maxPetrol = 100;
    public float petrolAmount = 100f; // Initial amount of petrol in liters
    private float depletionRate; // Rate at which petrol depletes in liters per second

    void Start()
    {
        // Calculate the depletion rate per second
        depletionRate = petrolAmount / 300f; // 3 minutes * 60 seconds = 180 seconds
        slider.maxValue = maxPetrol;
    }

    void Update()
    {
        // Decrease the petrolAmount based on depletion rate and time
        petrolAmount -= depletionRate * Time.deltaTime;

        // Ensure petrolAmount doesn't go below 0
        petrolAmount = Mathf.Max(petrolAmount, 0f);

        // Check if petrol has run out
        if (petrolAmount <= 0f)
        {
            // Petrol has run out, perform any necessary actions (e.g., stop a car)
            Debug.Log("Out of petrol!");
            // You can add more actions here, like stopping the engine, etc.
        }

        // Cap petrolAmount at maxPetrol
        petrolAmount = Mathf.Min(petrolAmount, maxPetrol);

        slider.value = petrolAmount;

    }
}
