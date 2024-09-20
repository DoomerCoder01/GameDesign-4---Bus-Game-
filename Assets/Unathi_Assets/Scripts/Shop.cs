using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Text textUI;
    public GameObject text;
    public GameObject yesButton;

    public FillingStation fillingStation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateText(string stringText)
    {
            textUI.text = stringText;
    }

    public void BuyGas()
    {            if (PassengerController.currency > 70)
        { 
            fillingStation.RefillGas();
            PassengerController.currency -= 70;
        }
        else
        {
            UpdateText("You do not have enough money");
            yesButton.SetActive(false);
        }
    }
}
