using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashScreenController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "Main Menu"; // Change this to your next scene

    void Start()
    {
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
