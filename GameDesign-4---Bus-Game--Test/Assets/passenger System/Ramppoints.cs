using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Required for IEnumerator

public class RampPoints : MonoBehaviour
{
    public int rampPoints = 10; // Points to award for hitting a ramp
    private bool rampTriggered = false; // Flag to track if ramp points have been awarded

    public Text scoreText; // Reference to your Text object

    private void Update()
    {
        scoreText = GameObject.Find("Staunt").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bus"))
        {
            // Player entered the ramp trigger, award points if not already awarded
            if (!rampTriggered)
            {
                AwardRampPoints();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bus"))
        {
            // Reset ramp trigger flag when player exits
            rampTriggered = false;
        }
    }

    private void AwardRampPoints()
    {
        scoreText.text = "+ R " + rampPoints; // Update the score text
        PassengerController.AddCurrency(rampPoints);
        Debug.Log("Hit ramp, awarding " + rampPoints + " points");
        rampTriggered = true; // Mark the ramp as triggered
        StartCoroutine(ClearScoreText()); // Start the coroutine to clear the score text with delay
    }

    // Coroutine to clear the score text after a delay
    private IEnumerator ClearScoreText()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second
        scoreText.text = ""; // Clear the score text after the delay
    }
}
