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

    public void LoadFourthLevel()
    {
        SceneManager.LoadScene("Level 4");
    }

    public void LoadSecondLevel()
    {
        SceneManager.LoadScene("Level 2");
        //Debug.Log("Unathi");
        //// Start loading "Level 2" and "Anele_Level 2" asynchronously
        //AsyncOperation asyncLoadLevel2 = SceneManager.LoadSceneAsync("Level 2", LoadSceneMode.Additive);
        //AsyncOperation asyncLoadAneleLevel2 = SceneManager.LoadSceneAsync("Anele_Level 2", LoadSceneMode.Additive);

        //// Wait until both scenes are loaded before unloading "Level 1"
        //StartCoroutine(WaitForSceneLoad(asyncLoadLevel2, asyncLoadAneleLevel2));
    }

    private IEnumerator WaitForSceneLoad(AsyncOperation asyncLoadLevel2, AsyncOperation asyncLoadAneleLevel2)
    {
        while (!asyncLoadLevel2.isDone || !asyncLoadAneleLevel2.isDone)
        {
            yield return null;
        }

        // Both scenes have loaded, now unload "Level 1"
        SceneManager.UnloadSceneAsync("Level 1");
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
