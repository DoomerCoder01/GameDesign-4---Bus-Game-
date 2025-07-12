using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SideEffectManager : MonoBehaviour
{
    public static SideEffectManager Instance;
    private float sleepySteeringOffset = 0f;

    public Petrol petrol;

    public CarHorn carHorn;

    public ZoomChaos zoomChaos;

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
        FuelDrain,
        SnailMode,
        FlatTire,
        FuelBlock,
        FunnyHorn,
        ZoomChaos,
        GhostBus,
        PassengerPanic,
        CloneMirage,
        // Removed PassengerPanic and other passenger effects
    }

    void TriggerPassengerPanic()
{
    if (PassengerController.passengerCount > 0)
    {
        PassengerController.ResetPassengerCount();
        Debug.Log("Passenger Panic! All passengers have been removed.");
    }
    else
    {
        Debug.Log("Passenger Panic triggered, but there were no passengers onboard.");
    }
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

            case SideEffect.SnailMode:
                Debug.Log("Activating Snail Mode!");
                StartCoroutine(SnailMode(car, 30f));
                break;

            case SideEffect.FlatTire:
                Debug.Log("Flat Tire activated!");
                StartCoroutine(FlatTire(car, 10f));
                break;

            case SideEffect.FuelBlock:
                Debug.Log("Fuel Block activated!");
                StartCoroutine(FuelBlockEffect());
                break;
            case SideEffect.FunnyHorn:
                Debug.Log("FunnyHorn is on!");
                StartCoroutine(CarHornEffect());
                break;
            case SideEffect.ZoomChaos:
                Debug.Log("ZoomChaos is on!");
                StartCoroutine(ZoomChaosEffect());
                break;

            case SideEffect.GhostBus:
                Debug.Log("Ghost Bus activated!");
                StartCoroutine(GhostBus(car.transform, 20f));
                break;

            case SideEffect.PassengerPanic:
                 Debug.Log("Passenger Panic activated!");
                 TriggerPassengerPanic();
                 break;

            case SideEffect.CloneMirage:
                Debug.Log("Clone Mirage activated!");
                StartCoroutine(SpawnCloneMirage(car.transform, 15f));
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

    IEnumerator SnailMode(RCC_CarControllerV3 car, float duration)
    {
        if (car == null) yield break;

        Debug.Log("Snail Mode started.");

        // Save original max speed
        float originalMaxSpeed = car.maxspeed;

        // Reduce to snail speed (e.g., 20 units)
        car.maxspeed = 20f;

        float timer = 0f;
        while (timer < duration)
        {
            // Optional: If you want to further slow acceleration, clamp velocity
            if (car.GetComponent<Rigidbody>() != null)
            {
                Rigidbody rb = car.GetComponent<Rigidbody>();
                if (rb.velocity.magnitude > 5f) // equivalent of ~18km/h
                {
                    rb.velocity = rb.velocity.normalized * 5f;
                }
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Restore max speed
        car.maxspeed = originalMaxSpeed;
        Debug.Log("Snail Mode ended.");
    }

    IEnumerator FlatTire(RCC_CarControllerV3 car, float duration)
    {
        if (car == null) yield break;

        Debug.Log("Flat Tire effect started.");

        float timer = 0f;

        // Randomly decide which side to pull (left = -1, right = +1)
        int direction = Random.value > 0.5f ? 1 : -1;

        // Lean angle (in degrees) to tilt the bus visually
        float leanAngle = 10f * direction;

        // Cache the original rotation
        Quaternion originalRotation = car.transform.rotation;

        while (timer < duration)
        {
            // Add slight unwanted steering
            car.steerInput += 0.2f * direction;

            // Lean the bus visually
            Quaternion leanRotation = originalRotation * Quaternion.Euler(0, 0, leanAngle);
            car.transform.rotation = Quaternion.Lerp(car.transform.rotation, leanRotation, Time.deltaTime * 2f);

            timer += Time.deltaTime;
            yield return null;
        }

        // Reset rotation (optional: you can smoothly lerp back if needed)
        car.transform.rotation = originalRotation;

        Debug.Log("Flat Tire effect ended.");
    }

    IEnumerator FuelBlockEffect()
    {
        if (petrol == null) yield break;

        Debug.Log("Fuel Block started. Fuel is frozen.");

        petrol.isFuelBlocked = true;

        yield return new WaitForSeconds(20f);

        petrol.isFuelBlocked = false;

        Debug.Log("Fuel Block ended. Fuel is now depleting again.");
    }

    IEnumerator CarHornEffect()
    {
        if (carHorn == null) yield break;

        Debug.Log("Funny Horn started.");

        carHorn.sideEffectOn = true;

        yield return new WaitForSeconds(20f);

        carHorn.sideEffectOn = false;

        Debug.Log("Funny Horn stopped.");
    }

    IEnumerator ZoomChaosEffect()
    {
        if (zoomChaos == null) yield break;

        Debug.Log("Experiencing Zoom Chaos.");

        zoomChaos.zoomChaos = true;

        yield return new WaitForSeconds(20f);

        zoomChaos.zoomChaos = false;

        Debug.Log("No more Zoom Chaos!");
    }

    IEnumerator GhostBus(Transform busTransform, float duration)
    {
        Debug.Log("Ghost Bus effect started.");

        Renderer[] renderers = busTransform.GetComponentsInChildren<Renderer>();
        List<Material[]> originalMaterials = new List<Material[]>();

        // Store original materials and apply transparent shader
        foreach (Renderer rend in renderers)
        {
            originalMaterials.Add(rend.materials);

            foreach (Material mat in rend.materials)
            {
                mat.SetFloat("_Mode", 2); // Fade mode
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;

                Color color = mat.color;
                color.a = 0.3f; // Transparency
                mat.color = color;
            }
        }

        yield return new WaitForSeconds(duration);

        // Restore original opacity
        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.materials)
            {
                Color color = mat.color;
                color.a = 1f;
                mat.color = color;

                // Optional: Reset shader if needed
            }
        }

        Debug.Log("Ghost Bus effect ended.");
    }

IEnumerator SpawnCloneMirage(Transform playerBus, float duration)
{
    Debug.Log("Spawning clone mirage.");

    // Instantiate a clone
    GameObject clone = Instantiate(playerBus.gameObject);

    // Position slightly to the right of the real bus
    clone.transform.position = playerBus.position + playerBus.right * 3f;

    // Remove clone behaviors that affect gameplay
    foreach (var cc in clone.GetComponents<RCC_CarControllerV3>())
        Destroy(cc);

    // Make clone transparent
    Renderer[] renderers = clone.GetComponentsInChildren<Renderer>();
    foreach (Renderer rend in renderers)
    {
        foreach (Material mat in rend.materials)
        {
            mat.SetFloat("_Mode", 2); // Fade mode
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;

            Color color = mat.color;
            color.a = 0.4f; // Transparent ghost clone
            mat.color = color;
        }
    }

    // Optional: Make the clone follow loosely
    float timer = 0f;
    Vector3 offset = clone.transform.position - playerBus.position;

    while (timer < duration && clone != null)
    {
        clone.transform.position = Vector3.Lerp(clone.transform.position, playerBus.position + offset, Time.deltaTime * 5f);
        clone.transform.rotation = Quaternion.Lerp(clone.transform.rotation, playerBus.rotation, Time.deltaTime * 5f);
        timer += Time.deltaTime;
        yield return null;
    }

    if (clone != null)
        Destroy(clone);

    Debug.Log("Clone Mirage ended.");
}



}
