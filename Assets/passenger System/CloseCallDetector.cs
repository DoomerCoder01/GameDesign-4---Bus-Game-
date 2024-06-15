using UnityEngine;
using UnityEngine.UI;

public class CloseCallDetector : MonoBehaviour
{
    public int closeCallPoints = 10; // Points to award for a close call
    public Text scoreText; // Reference to your Text object

    private bool isCloseCall = false; // Flag to track if we're in a close call

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CloseCallTrigger") && !isCloseCall)
        {
            // We're in a close call
            isCloseCall = true;
            Debug.Log("Close call started with " + other.transform.parent.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CloseCallTrigger") && isCloseCall)
        {
            // Check if the close call was registered and no collision occurred
            Debug.Log("Vehicle exited close call trigger");

            if (!CheckForCollision())
            {
                Debug.Log("No collision detected, awarding close call points");
                AwardCloseCallPoints();
            }
            else
            {
                Debug.Log("Collision detected, no points awarded");
            }
            isCloseCall = false;
        }
    }

    private bool CheckForCollision()
    {
        // Check if the parent collider has collided with any vehicle
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f); // Using half the scale of the transform for the box size
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Vehicle"))
            {
                return true; // Collision detected
            }
        }
        return false; // No collision detected
    }

    private void AwardCloseCallPoints()
    {
        scoreText.text = "+" + closeCallPoints; // Update the score text
        // Add your logic to award points or rewards here
        Debug.Log("Close call passed, awarding " + closeCallPoints + " points");
        ClearScoreText(); // Clear the score text immediately
    }

    // Method to clear the score text immediately
    private void ClearScoreText()
    {
        scoreText.text = ""; // Clear the score text
    }
}