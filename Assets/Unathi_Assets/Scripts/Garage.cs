using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Garage : MonoBehaviour
{
    public int countDown;
    public Text countText;

     bool isCounting = false; // Flag to track if the countdown is actively counting down

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name);
        countDown = 200; // Set initial countdown value
        InvokeRepeating("DecrementCountdown", 1f, 1f); // Removed this line
    }

    // Update is called once per frame
    void Update()
    {
        countText.text = countDown.ToString();
        Debug.Log(countDown);
    }

    public void StartCountdown()
    {
        isCounting = true; // Set the flag to indicate that the countdown is active
        InvokeRepeating("DecrementCountdown", 1f, 1f); // Start countdown every second
    }

    void StopCountdown()
    {
        isCounting = false; // Set the flag to indicate that the countdown is not active
        CancelInvoke("DecrementCountdown"); // Stop the countdown timer
    }

    void DecrementCountdown()
    {
        countDown--; // Decrement countdown every second

        if (countDown <= 0)
        {
            StopCountdown(); // Stop the countdown when it reaches 0
            countDown = 0; // Ensure countdown doesn't go below 0

            // Reset canTrigger flag in FillingStation script
            FindObjectOfType<FillingStation>().ResetCanTrigger();
        }
    }
}
