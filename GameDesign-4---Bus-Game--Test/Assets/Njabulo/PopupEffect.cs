using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupEffect : MonoBehaviour
{
    public GameObject popupPanel;
    public Text popupText;

    public void ShowPopup(string message)
    {
        popupPanel.SetActive(true);
        popupText.text = message;
        StartCoroutine(HidePopup());
    }

    IEnumerator HidePopup()
    {
        yield return new WaitForSeconds(2f);
        popupPanel.SetActive(false);
    }
}
