using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingStation : MonoBehaviour
{
    [SerializeField] Petrol petrol;

    [SerializeField] Garage garage;

    [SerializeField] bool canTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                    if (garage.countDown >  0)
        {
            canTrigger = false;
        }                      else if (garage.countDown <= 0)
        {
            canTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bus") && canTrigger)
        {
            garage.countDown += 200; // Reset countdown when bus enters trigger
            petrol.petrolAmount += petrol.maxPetrol / 2;
            garage.StartCountdown(); // Start the countdown timer
            canTrigger = false; // Disable triggering until the countdown reaches 0 again
        }
    }

    // Method to reset the canTrigger flag when countdown is reset to 10
    public void ResetCanTrigger()
    {
        canTrigger = true;
    }
}
