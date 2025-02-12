using UnityEngine;
using System.Collections;

public class UnderwaterEffect : MonoBehaviour
{
    public Camera playerCamera; // Reference to the player's camera
    public Color underwaterColor = new Color(0f, 0.3f, 0.6f); // Underwater background color
    public float underwaterFogDensity = 0.1f; // Underwater fog density
    public float fadeSpeed = 2f; // Speed of the fade effect

    private Color originalColor; // To store the original background color
    private float originalFogDensity; // To store the original fog density
    private bool isUnderwater = false; // To track if the player is underwater

    void Start()
    {
        // Store the original settings
        originalColor = playerCamera.backgroundColor;
        originalFogDensity = RenderSettings.fogDensity;
    }

    // Detect when the player enters the water
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure it's the player
        {
            EnterWater();
        }
    }

    // Detect when the player exits the water
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure it's the player
        {
            ExitWater();
        }
    }

    // Call this method when the player enters the water
    public void EnterWater()
    {
        if (!isUnderwater)
        {
            isUnderwater = true; // Mark player as underwater
            StartCoroutine(ApplyUnderwaterEffects());
        }
    }

    // Call this method when the player exits the water
    public void ExitWater()
    {
        if (isUnderwater)
        {
            isUnderwater = false; // Mark player as above water
            StartCoroutine(RemoveUnderwaterEffects());
        }
    }

    private IEnumerator ApplyUnderwaterEffects()
    {
        while (playerCamera.backgroundColor != underwaterColor)
        {
            // Smoothly transition to the underwater color
            playerCamera.backgroundColor = Color.Lerp(playerCamera.backgroundColor, underwaterColor, Time.deltaTime * fadeSpeed);

            // Smoothly transition to underwater fog density
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, underwaterFogDensity, Time.deltaTime * fadeSpeed);

            // Enable fog when underwater
            RenderSettings.fog = true;

            yield return null;
        }
    }

    private IEnumerator RemoveUnderwaterEffects()
    {
        while (playerCamera.backgroundColor != originalColor)
        {
            // Smoothly transition back to the original color
            playerCamera.backgroundColor = Color.Lerp(playerCamera.backgroundColor, originalColor, Time.deltaTime * fadeSpeed);

            // Smoothly transition back to no fog
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, 0f, Time.deltaTime * fadeSpeed);

            yield return null;
        }

        // Disable fog above water
        RenderSettings.fog = false;
    }
}
