using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Manager : MonoBehaviour
{
    public int passengerCount;

    public Text lvl3UnlockText;

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
            lvl3UnlockText.text = "Unlock Level 3 (" + passengerCount + "/5)";
        }
        else
        {
            lvl3UnlockText.text = "Level 3";

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (passengerCount >= 5)
            sceneManagement.ActivatePanel();
    }

    void OnTriggerExit(Collider other)
    {
        sceneManagement.DeactivatePanel();
    }
}
