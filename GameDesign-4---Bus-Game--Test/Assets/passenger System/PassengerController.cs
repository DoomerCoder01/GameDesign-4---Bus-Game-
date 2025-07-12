using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PassengerController : MonoBehaviour
{
    [SerializeField] GameObject greenBox;

    public GameObject playerVehicle; // Assign your player vehicle in the Inspector
    public float speed = 5f; // Speed at which the passenger moves towards the vehicle
    public GameObject passengerModel; // The passenger model
    public bool isMoving = false;
    public Animator animator; // The Animator component
    [SerializeField] public static int passengerCount = 0; // Passenger counter
    public Text passengerCountText; // Assign your Text UI component in the Inspector
    public static int currency = 0; // Static variable to keep track of currency

    public delegate void CurrencyChanged(int newCurrencyAmount);
    public static event CurrencyChanged OnCurrencyChanged;

    void Start()
    {
        passengerModel = transform.GetChild(0).gameObject; // Get the first child of the object
        animator = passengerModel.GetComponent<Animator>(); // Get the Animator component from the passenger model
    }

    void Update()
    {
        // Debug.Log("passenger count: " + passengerCount.ToString());

        // Check if passengerCountText is assigned before updating it
        if (passengerCountText != null)
        {
            passengerCountText.text = passengerCount.ToString(); // Update the passenger count text
        }
        else
        {
            Debug.LogWarning("PassengerCountText is not assigned in the Inspector.");
        }

        if (isMoving)
        {
            // Move the passenger towards the player vehicle
            float step = speed * Time.deltaTime;
            passengerModel.transform.position = Vector3.MoveTowards(passengerModel.transform.position, playerVehicle.transform.position, step);

            // Check if the passenger has reached the player vehicle
            if (Vector3.Distance(passengerModel.transform.position, playerVehicle.transform.position) < 0.001f)
            {
                isMoving = false;
                animator.SetBool("GetOn", false); // Stop the walking animation
                passengerModel.SetActive(false); // Disable the passenger model
                passengerCount++; // Increase the passenger count

                greenBox.SetActive(false);

                if (passengerCountText != null)
                {
                    passengerCountText.text = passengerCount.ToString(); // Update the passenger count text
                }
            }
        }
    }

    public static void AddCurrency(int amount)
    {
        currency += amount;
        OnCurrencyChanged?.Invoke(currency); // Notify listeners that currency has changed
    }

    public static void ResetPassengerCount()
{
    passengerCount = 0;

    // Update all active PassengerControllers' UI
    PassengerController[] allControllers = FindObjectsOfType<PassengerController>();
    foreach (PassengerController pc in allControllers)
    {
        if (pc.passengerCountText != null)
        {
            pc.passengerCountText.text = "0";
        }
    }

    Debug.Log("Passenger Panic: All passengers removed. Count reset to 0.");
}

}


