using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public Transform spawnPoint; // Assign your empty GameObject here in the Inspector

    public float spawnInterval = 6f;
    public float heightAboveGround = 0.5f;

    private float timer = 0f;

    void Start()
    {
        Debug.Log("PowerUpSpawner location: "+gameObject.name);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPowerUpAtPoint();
            timer = 0f;
        }
    }

    void SpawnPowerUpAtPoint()
    {
        if (spawnPoint == null || powerUpPrefab == null) return;

        Vector3 spawnPos = spawnPoint.position + Vector3.up * heightAboveGround;

        GameObject powerUp = Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);
        powerUp.name = "PowerUp_" + Time.time;

        Debug.Log("Spawned power-up at: " + spawnPos);
    }

}
