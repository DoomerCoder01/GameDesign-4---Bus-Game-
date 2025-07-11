using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHorn : MonoBehaviour
{
    public AudioSource goatHorn;
    public AudioSource normalHorn;// Drag an AudioSource here in the Inspector

    public bool sideEffectOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (goatHorn != null && normalHorn != null)
            {
                if (sideEffectOn)
                {
                    goatHorn.Play();
                }      else
                {
                    normalHorn.Play();
                }
            }
        }
    }
}
