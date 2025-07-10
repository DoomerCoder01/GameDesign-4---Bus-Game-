using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public int numberOfPowerUps = 15;
    public Vector2 spawnAreaMin = new Vector2(-50, -50);
    public Vector2 spawnAreaMax = new Vector2(50, 50);

    void Start()
    {
        for (int i = 0; i < numberOfPowerUps; i++)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                1f, // height above ground
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );
            Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);
        }
    }


}
