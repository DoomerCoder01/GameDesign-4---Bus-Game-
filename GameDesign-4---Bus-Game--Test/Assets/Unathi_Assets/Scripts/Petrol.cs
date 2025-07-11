using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Petrol : MonoBehaviour
{
    public Slider slider;
    public float maxPetrol = 100f;
    public float petrolAmount = 100f; // Initial amount of petrol in liters
    private float depletionRate; // Rate at which petrol depletes in liters per second
    public bool isFuelBlocked = false;
    public bool isMoving;


    [SerializeField] Text petrolText;
    [SerializeField] GameObject petrolTExt;

    void Start()
    {
        // Calculate the depletion rate per second
        depletionRate = petrolAmount / 300f; // 3 minutes * 60 seconds = 180 seconds
        slider.maxValue = maxPetrol;
    }

    void Update()
    {
        // Check if any movement key is being held
        isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                   Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (!isFuelBlocked && isMoving)
        {
            petrolAmount -= depletionRate * Time.deltaTime;
        }

        // Clamp petrolAmount between 0 and maxPetrol
        petrolAmount = Mathf.Clamp(petrolAmount, 0f, maxPetrol);

        // Update slider UI
        slider.value = petrolAmount;

        if (petrolAmount <= 0f)
        {
            petrolTExt.SetActive(true);
            petrolText.text = "OUT OF PETROL!";
        }
        else
        {
            petrolTExt.SetActive(false);
        }
    }

    public void ReduceFuelByPercentage(float percent)
    {
        float amountToDrain = maxPetrol * (percent / 100f);
        petrolAmount = Mathf.Max(0f, petrolAmount - amountToDrain);
        slider.value = petrolAmount; // Ensure UI updates immediately
        Debug.Log("Fuel reduced by {percent}% (-{amountToDrain}L). New level: {petrolAmount}L");

        if (petrolAmount <= 0f)
        {
            petrolTExt.SetActive(true);
            petrolText.text = "OUT OF PETROL!";
        }
    }

}
