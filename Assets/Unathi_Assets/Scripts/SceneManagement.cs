using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour
{
   [SerializeField] GameObject panel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject book;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadSecondLevel()
    {
        Debug.Log("Unathi");
        SceneManager.LoadScene("Level 2");
        //SceneManager.LoadScene("Anele_Level 2");
    }

    public void LoadMainMenu()
    {
        Debug.Log("Menu");
        SceneManager.LoadScene("Main Menu");
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

    public void ActivateMenuPanel()
    {
        Time.timeScale = 0;
        menuPanel.SetActive(true);
    }

    public void DeactivateMenuPanel()
    {
        Time.timeScale = 1;
        menuPanel.SetActive(false);
    }

    public void ActivateBook()
    {
        book.SetActive(true);
    }

    public void DeactivateBook()
    {
        book.SetActive(false);
    }
}
