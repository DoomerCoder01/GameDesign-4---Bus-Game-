using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private bool isPaused;

    public GameObject panel;

    void Start()
    {
        PauseGame();
}

    // Call this to pause the game
    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
            Debug.Log("Game Paused");
        }
    }

    // Call this to unpause the game
    public void UnpauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
            Debug.Log("Game Unpaused");
        }

        panel.SetActive(false);
    }
}
