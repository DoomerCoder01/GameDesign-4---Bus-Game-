using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // This method is called when the script instance is being loaded
    void Awake()
    {
        // Ensure that this GameObject is not destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
    }
}
