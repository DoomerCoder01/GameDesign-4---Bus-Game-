using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PassengerController : MonoBehaviour
{
    public GameObject playerVehicle; // Assign your player vehicle in the Inspector
    public float speed = 5f; // Speed at which the passenger moves towards the vehicle
    public GameObject passengerModel; // The passenger model
    public bool isMoving = false;
    public Animator animator; // The Animator component
    [SerializeField] public static int passengerCount = 0; // Passenger counter
    public Text passengerCountText; // Assign your TextMeshPro GameObject in the Inspector

    void Start()
    {
        passengerModel = transform.GetChild(0).gameObject; // Get the first child of the object
        animator = passengerModel.GetComponent<Animator>(); // Get the Animator component from the passenger model
    }

    void Update()
    {
        Debug.Log("passenger count" + passengerCount.ToString());
        passengerCountText.text = passengerCount.ToString(); // Update the passenger count text
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
                passengerCountText.text = passengerCount.ToString(); // Update the passenger count text
            }
        }
    }
}


