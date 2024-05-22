using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using System.Collections; // Import the System.Collections namespace

public class CloseCallDetector : MonoBehaviour
{
    public float closeCallDistance = 5f; // Distance to consider a close call
    public int rampPoints = 10; // Points to award for hitting a ramp
    public int closeCallPoints = 10; // Points to award for a close call
    private bool isCloseCall = false; // Flag to track if we're in a close call

    public TextMeshProUGUI scoreText; // Reference to your TextMeshProUGUI object

    void FixedUpdate()
    {
        // Use a sphere cast to detect close vehicles
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, closeCallDistance, transform.forward, out hit))
        {
            // Check if the detected object is a vehicle
            if (hit.collider.CompareTag("Vehicle"))
            {
                // Check if the vehicle is on the side of the player
                Vector3 relativePos = transform.InverseTransformPoint(hit.transform.position);
                if (Mathf.Abs(relativePos.x) > Mathf.Abs(relativePos.z))
                {
                    // We're in a close call
                    isCloseCall = true;
                    Debug.Log("Close call with " + hit.collider.name);
                }
            }
        }
        else if (isCloseCall)
        {
            // We were in a close call but now we're not, award points
            scoreText.text = "Score: " + closeCallPoints; // Update the score text
            Debug.Log("Close call passed, awarding " + closeCallPoints + " points");
            StartCoroutine(ClearScoreText()); // Clear the score text after a delay
            isCloseCall = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if we collided with a vehicle during a close call
        if (isCloseCall && collision.collider.CompareTag("Vehicle"))
        {
            // We hit a vehicle during a close call, don't award points
            Debug.Log("Hit " + collision.collider.name + " during close call, no points awarded");
            isCloseCall = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if we entered a trigger tagged with "Ramp"
        if (other.CompareTag("Ramp"))
        {
            // We hit a ramp, award points
            scoreText.text = "+ R " + rampPoints; // Update the score text
            Debug.Log("Hit ramp, awarding " + rampPoints + " points");
            StartCoroutine(ClearScoreText()); // Clear the score text after a delay
        }
    }

    // Draw Gizmo
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, closeCallDistance);
    }

    // Coroutine to clear the score text after a delay
    IEnumerator ClearScoreText()
    {
        yield return new WaitForSeconds(2); // Wait for 2 seconds
        scoreText.text = ""; // Clear the score text
    }
}
