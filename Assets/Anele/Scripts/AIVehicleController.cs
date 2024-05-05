using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVehicleController : MonoBehaviour
{

    public float speed = 305f;

    // Start is called before the first frame update
    void Start()
    {
        StartMoving();
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }

    void MoveForward()
    {
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void StartMoving()
    {
       
        Debug.Log("Vehicle starts moving!");
        enabled = true;
    }
}
