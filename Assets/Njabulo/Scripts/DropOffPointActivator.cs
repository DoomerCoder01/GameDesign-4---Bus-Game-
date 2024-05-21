using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffPointActivator : MonoBehaviour
{
    public GameObject[] dropOffPoints; 

    public void ActivateDropOffPoints()
    {
        foreach (GameObject dropOffPoint in dropOffPoints)
        {
            dropOffPoint.SetActive(true);
        }
    }
}
