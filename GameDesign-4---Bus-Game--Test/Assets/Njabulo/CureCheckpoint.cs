using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureCheckpoint : MonoBehaviour
{
    private EffectLogger logger;
    private PopupEffect popup;

    public int checkpointNumber; // 1, 2, or 3
    public CureCheckpointManager manager;

    void Start()
    {
        logger = FindObjectOfType<EffectLogger>();
        popup = FindObjectOfType<PopupEffect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bus"))
        {
            // Send info to manager
            manager.CheckpointReached(this);

            // Destroy checkpoint
            Destroy(gameObject);
        }
    }

}
