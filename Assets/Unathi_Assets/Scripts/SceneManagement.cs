using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour
{
   [SerializeField] GameObject panel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadFirstLevel()
    {
        Debug.Log("Unathi");
        SceneManager.LoadScene("IntroLevel");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ActivatePanel()
    {
        panel.SetActive(true); 
    }

    public void DeactivatePanel()
    {
        panel.SetActive(false);
    }
}
