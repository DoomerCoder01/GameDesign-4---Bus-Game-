using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Win_Lose : MonoBehaviour
{
    public int passengers;
    [SerializeField] Text passengerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        passengerText.text = passengers.ToString() + " / 9";
    }
}
