using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SideEffectManager : MonoBehaviour
{
  public static SideEffectManager Instance;
  private float sleepySteeringOffset = 0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public enum SideEffect
    {
        TurboBoost,
        SleepySteering,
        BouncyBrakes,
        ReverseControls,
        FuelDrain
        // Removed PassengerPanic and other passenger effects
    }

    public void ApplyRandomSideEffect(GameObject player)
    {
        SideEffect effect = (SideEffect)Random.Range(0, System.Enum.GetValues(typeof(SideEffect)).Length);
        ApplyEffect(effect, player);
    }

    public void ApplyEffect(SideEffect effect, GameObject player)
    {
        var car = player.GetComponent<RCC_CarControllerV3>();
        var petrol = player.GetComponent<Petrol>();

        Debug.Log("Activated Side Effect: " + effect.ToString());

        switch (effect)
        {
            case SideEffect.TurboBoost:
                StartCoroutine(TurboBoost(car, 30f));
                break;

            case SideEffect.SleepySteering:
                StartCoroutine(SleepySteering(car, 10f));
                break;

            case SideEffect.BouncyBrakes:
                StartCoroutine(BouncyBrakes(car, 5f));
                break;

            case SideEffect.ReverseControls:
                StartCoroutine(ReverseControls(car, 15f));
                break;

            case SideEffect.FuelDrain:
                if (petrol != null)
                    petrol.ReduceFuelByPercentage(40f);
                break;
        }
    }

    IEnumerator TurboBoost(RCC_CarControllerV3 car, float duration, float multiplier = 2f)
    {
        if (car == null) yield break;

        float originalMaxSpeed = car.maxspeed;
        car.maxspeed *= multiplier;

        yield return new WaitForSeconds(duration);

        car.maxspeed = originalMaxSpeed;
        
        
    Debug.Log("TurboBoost effect started.");
    }

    IEnumerator SleepySteering(RCC_CarControllerV3 car, float duration)
    {
        if (car == null) yield break;

        
    Debug.Log("SleepySteering effect started.");

    float timer = 0f;
    while (timer < duration)
    {
        sleepySteeringOffset = Mathf.Sin(Time.time * 3f) * 0.3f; // Bigger offset if you want
        car.steerInput = Mathf.Clamp(car.steerInput + sleepySteeringOffset, -1f, 1f);

        timer += Time.deltaTime;
        yield return null;
    }

    sleepySteeringOffset = 0f;

    }

    IEnumerator BouncyBrakes(RCC_CarControllerV3 car, float duration)
    {
        if (car == null)
        
    {
        Debug.LogWarning("Car reference is null!");
        yield break;
    }

    Rigidbody rb = car.GetComponent<Rigidbody>();
    if (rb == null)
    {
        Debug.LogWarning("No Rigidbody found on car!");
        yield break;
    }

    float timer = 0f;

        while (timer < duration)
        {
            // Debug to check brake input values
            Debug.Log($"Brake input: {car.brakeInput}");

            if (car.brakeInput > 0.5f)
            {
                // Apply an upward force as an impulse for a stronger bounce effect
                rb.AddForce(Vector3.up * 2500f, ForceMode.Impulse);
            }

            timer += Time.deltaTime;
            yield return null;
        
    Debug.Log("BouncyBrakes effect started.");
    }

    }

    IEnumerator ReverseControls(RCC_CarControllerV3 car, float duration)
    {
        if (car == null) yield break;

    float timer = 0f;
    Debug.Log("ReverseControls effect started.");

    while (timer < duration)
    {
        // Assuming car.steerInput is overwritten by player input every frame,
        // forcibly invert it after that happens:

        float currentInput = car.steerInput; // get current input
        car.steerInput = -currentInput;      // invert it

        timer += Time.deltaTime;
        yield return null;
    }

    Debug.Log("ReverseControls effect ended.");

    }

}
