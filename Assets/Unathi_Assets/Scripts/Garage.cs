using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Garage : MonoBehaviour
{
   
    public int countDown;

    [SerializeField] Text countText;

    bool canTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        countDown = 200; // Set initial countdown value
        InvokeRepeating("DecrementCountdown", 1f, 1f); // Start countdown every second
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown <= 0)
        {
            canTrigger = true; // Enable triggering when countdown reaches zero
        }
        else
        {
            canTrigger = false; // Disable triggering while countdown is running
        }

        countText.text = countDown.ToString();
    }

    void DecrementCountdown()
    {
        countDown--; // Decrement countdown every second
    }
}
