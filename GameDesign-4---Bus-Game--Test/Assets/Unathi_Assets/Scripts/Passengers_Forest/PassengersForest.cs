using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengersForest : MonoBehaviour
{
    public static PassengersForest Instance;

    public int passengersCollected;
    public Text passengerCount;

    private List<GameObject> passengers = new List<GameObject>();
    [SerializeField] private GameObject passengerPrefab; // The prefab to spawn
    public Transform[] spawnPoints;    // All spawn points

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        SpawnPassengers(); // Spawn passengers at the beginning
    }

    void Start()
    {
        Debug.Log("PassengersForest Location: " + gameObject.name);
    }

    void Update()
    {
        passengerCount.text = passengersCollected.ToString();
    }

    private void SpawnPassengers()
    {
        passengers.Clear();

        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject newPassenger = Instantiate(passengerPrefab, spawnPoint.position, spawnPoint.rotation);
            passengers.Add(newPassenger);
        }
    }

    IEnumerator AllPassengersCollected()
    {
        yield return new WaitForSeconds(20);

        SpawnPassengers(); // Respawn all passengers when collected
    }

    public void PassengerCollected(GameObject passenger)
    {
        passengers.Remove(passenger);
        Destroy(passenger.gameObject);

        passengersCollected++;

        if (passengers.Count == 0)
        {
             StartCoroutine(AllPassengersCollected());
        }
    }
}
