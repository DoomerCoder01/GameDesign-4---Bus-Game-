using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffect : MonoBehaviour
{
     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bus"))
        {
            SideEffectManager.Instance.ApplyRandomSideEffect(other.gameObject);
            Destroy(gameObject); // Remove the power-up
        }
    }
}
