using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
  public GameObject powerUpPrefab;
    public Transform player; // Your bus or player transform

    public float spawnInterval = 6f;
    public float forwardDistance = 60f;   // Distance in front of bus
    public float sidewaysRange = 20f;     // Range left/right
    public float heightAboveGround = 0.5f; // Distance above ground

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPowerUpAhead();
            timer = 0f;
        }
    }

    void SpawnPowerUpAhead()
    {
        if (player == null || powerUpPrefab == null) return;

        // Base spawn position in front of player
        Vector3 basePos = player.position + player.forward * forwardDistance;

        // Random horizontal offset
        basePos += player.right * Random.Range(-sidewaysRange, sidewaysRange);
        basePos.y += 20f; // Start the raycast from above terrain

        // Raycast down to the terrain
        if (Physics.Raycast(basePos, Vector3.down, out RaycastHit hit, 50f))
        {
            Vector3 spawnPos = hit.point + Vector3.up * heightAboveGround;
            GameObject powerUp = Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);
            powerUp.name = "PowerUp_" + Time.time;

            Debug.Log("Spawned power-up at: " + spawnPos);
        }
        else
        {
            Debug.LogWarning("No ground found below spawn point.");
        }
    }

}
