using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Manager : MonoBehaviour
{
    public int passengerCount;

    public Text lvl2UnlockText;

    SceneManagement sceneManagement;

    // Start is called before the first frame update
    void Start()
    {
        sceneManagement = FindObjectOfType<SceneManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (passengerCount < 5)
        {
            lvl2UnlockText.text = "Unlock Level 2 (" + passengerCount+ "/5)";
        }
        else
        {
            lvl2UnlockText.text = "Level 2";

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(passengerCount >= 5) 
        sceneManagement.ActivatePanel();
    }

    void OnTriggerExit(Collider other)
    {
            sceneManagement.DeactivatePanel();
    }
}
