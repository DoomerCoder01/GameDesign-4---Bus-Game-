using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingStation : MonoBehaviour
{
    [SerializeField] Petrol petrol;

    [SerializeField] Garage garage;

    [SerializeField] bool canTrigger;

    Shop shop;

    // Start is called before the first frame update
    void Start()
    {
        shop = GameObject.FindGameObjectWithTag("Bus").GetComponent<Shop>();
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
            shop.fillingStation = gameObject.GetComponent<FillingStation>();
            shop.text.SetActive(true);
            shop.yesButton.SetActive(true);
            shop.UpdateText("Do you want to refill? (70)");
        }
    }

    void OnTriggerExit(Collider other)
    {
        shop.text.SetActive(false);
    }

    // Method to reset the canTrigger flag when countdown is reset to 10
    public void ResetCanTrigger()
    {
        canTrigger = true;
    }

    public void RefillGas()
    {
        if(!canTrigger)
            return;

        garage.countDown += 200; // Reset countdown when bus enters trigger
        petrol.petrolAmount += petrol.maxPetrol;
        garage.StartCountdown(); // Start the countdown timer
        canTrigger = false; // Disable triggering until the countdown reaches 0 again
    }
}
