using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public Text currencyText; // Assign this in the Inspector

    void Start()
    {
        // Initialize the currency display
        UpdateCurrencyDisplay(PassengerController.currency);
    }

    void Update()
    {
        currencyText.text = "Currency: " + PassengerController.currency.ToString();
        Debug.Log(gameObject.name);
    }

    public void UpdateCurrencyDisplay(int newCurrencyAmount)
    {
        currencyText.text = "Currency: " + newCurrencyAmount.ToString();
    }

    void OnEnable()
    {
        PassengerController.OnCurrencyChanged += UpdateCurrencyDisplay;
    }

    void OnDisable()
    {
        PassengerController.OnCurrencyChanged -= UpdateCurrencyDisplay;
    }
}