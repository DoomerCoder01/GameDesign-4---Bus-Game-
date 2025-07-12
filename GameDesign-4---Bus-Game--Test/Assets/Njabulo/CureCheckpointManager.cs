using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CureCheckpointManager : MonoBehaviour
{
    public Text timerText;
    public GameObject checkpointPrefab;
    public Transform[] spawnPoints; // Assign 9 spawn points in inspector
    public string[] sideEffectNames = { "Passenger Panic", "Route Controls", "Bouncy Breaks" };
    public float timeLeft = 15f;

    public PowerUpSpawner powerUpSpawner;

    private int curesCollectedInBatch = 0;
    private const int curesPerBatch = 3;

    private EffectLogger logger;
    private PopupEffect popup;

    private List<GameObject> activeCheckpoints = new List<GameObject>();

    void Start()
    {
        logger = FindObjectOfType<EffectLogger>();
        popup = FindObjectOfType<PopupEffect>();

        timerText.text = "";
        SpawnBatch();
    }

    void SpawnBatch()
    {
        curesCollectedInBatch = 0;

        // Destroy any leftover checkpoints just in case
        foreach (var cp in activeCheckpoints)
        {
            if (cp != null)
                Destroy(cp);
        }
        activeCheckpoints.Clear();

        List<int> usedIndices = new List<int>();

        for (int i = 0; i < curesPerBatch; i++)
        {
            int spawnIndex;
            do
            {
                spawnIndex = Random.Range(0, spawnPoints.Length);
            } while (usedIndices.Contains(spawnIndex));
            usedIndices.Add(spawnIndex);

            GameObject cp = Instantiate(checkpointPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
            CureCheckpoint cureScript = cp.GetComponent<CureCheckpoint>();
            cureScript.manager = this;
            cureScript.checkpointNumber = i + 1;

            activeCheckpoints.Add(cp);
        }
    }

    public void CheckpointReached(CureCheckpoint checkpoint)
    {
        if (activeCheckpoints.Contains(checkpoint.gameObject))
            activeCheckpoints.Remove(checkpoint.gameObject);

        curesCollectedInBatch++;

        if (curesCollectedInBatch <= sideEffectNames.Length)
        {
            string effectName = sideEffectNames[curesCollectedInBatch - 1];
            logger.LogEffect("Current Side Effect: " + effectName);
            popup.ShowPopup("Side Effect: " + effectName);
        }

        switch (curesCollectedInBatch)
        {
            case 1:
                logger.LogEffect("? Cure 1/3 collected � your symptoms are calming down�");
                popup.ShowPopup("CURE 1/3 COLLECTED!");
                break;
            case 2:
                logger.LogEffect("? Cure 2/3 collected � you're getting better�");
                popup.ShowPopup("CURE 2/3 COLLECTED!");
                break;
            case 3:
                logger.LogEffect("? All side effects cured! Stay focused and reach the last stop.");
                popup.ShowPopup("ALL SIDE EFFECTS CURED!");
                StartCoroutine(TemporaryRelief());
                break;
        }
    }

    private IEnumerator TemporaryRelief()
    {
        // ✅ Disable PowerUp Spawner
        if (powerUpSpawner != null)
        {
            powerUpSpawner.enabled = false;
            Debug.Log("PowerUpSpawner DISABLED");
        }

        popup.ShowPopup("10s: No side effects!");
        Debug.Log("Side effects OFF");

        float timeLeft = 10f;
        while (timeLeft > 0)
        {
            timerText.text = "Relief: " + Mathf.CeilToInt(timeLeft).ToString() + "s";
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        timerText.text = "";
        Debug.Log("Side effects ON");
        popup.ShowPopup("Side effects are back!");

        // ✅ Re-enable PowerUp Spawner
        if (powerUpSpawner != null)
        {
            powerUpSpawner.enabled = true;
            Debug.Log("PowerUpSpawner ENABLED");
        }

        SpawnBatch(); // Respawn cures
    }
}
