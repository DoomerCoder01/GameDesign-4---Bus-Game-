using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropOffController : MonoBehaviour
{
    public GameObject[] dropOffPoints; // Assign your drop off points in the Inspector
    public Slider[] timerSliders; // Assign your timer sliders in the Inspector
    public GameObject passengerModel; // Assign your passenger model in the Inspector
    private GameObject currentDropOffPoint;
    public GameObject PlayerVehicle;

    void Start()
    {
        AssignRandomDropOffPoint();
    }

    void AssignRandomDropOffPoint()
    {
        // Assign a random drop off point that is not the closest one
        GameObject closestDropOffPoint = GetClosestDropOffPoint();
        do
        {
            currentDropOffPoint = dropOffPoints[Random.Range(0, dropOffPoints.Length)];
        } while (currentDropOffPoint == closestDropOffPoint);
    }

    GameObject GetClosestDropOffPoint()
    {
        GameObject closestDropOffPoint = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject dropOffPoint in dropOffPoints)
        {
            float distance = Vector3.Distance(passengerModel.transform.position, dropOffPoint.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestDropOffPoint = dropOffPoint;
            }
        }
        return closestDropOffPoint;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player vehicle has entered this object's trigger
        if (other.gameObject == PlayerVehicle)
        {
            // Find the slider associated with the triggered drop-off point
            int index = System.Array.IndexOf(dropOffPoints, other.gameObject);
            if (index != -1)
            {
                StartCoroutine(StartTimer(timerSliders[index]));
            }
        }
    }

    IEnumerator StartTimer(Slider timerSlider)
    {
        float timeRemaining = 3f;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerSlider.value = timeRemaining / 3f;
            yield return null;
        }
        InstantiatePassenger();
    }

    void InstantiatePassenger()
    {
        GameObject newPassenger = Instantiate(passengerModel, currentDropOffPoint.transform.position, Quaternion.identity);
        Destroy(newPassenger, 6f);
    }
}
