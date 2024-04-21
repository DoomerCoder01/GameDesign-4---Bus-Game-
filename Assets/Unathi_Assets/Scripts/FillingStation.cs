using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingStation : MonoBehaviour
{
    [SerializeField] Petrol petrol;

    [SerializeField] Garage garage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bus"))
        {
            garage.countDown = 200; // Reset countdown when bus enters trigger
            petrol.petrolAmount += petrol.maxPetrol / 2;
        }
    }
}
