using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    public ParticleSystem bloodEffectPrefab; // Assign your blood effect prefab in the inspector

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the NPC has collided with an object tagged "Bus"
        if (collision.gameObject.CompareTag("Bus"))
        {
            // Instantiate the blood effect at the NPC's position and rotation
            ParticleSystem bloodEffectInstance = Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);

            // Play the blood effect
            bloodEffectInstance.Play();

            // Destroy the blood effect after it has finished playing
            Destroy(bloodEffectInstance.gameObject, bloodEffectInstance.main.duration);

            // Destroy the NPC
            Destroy(gameObject);
        }
    }
}
