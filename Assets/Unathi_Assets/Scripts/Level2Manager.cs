using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Manager : MonoBehaviour
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
            lvl2UnlockText.text = "Level 2";
    }

    void OnTriggerEnter(Collider other)
    {
            sceneManagement.ActivatePanel();
    }

    void OnTriggerExit(Collider other)
    {
        sceneManagement.DeactivatePanel();
    }
}
