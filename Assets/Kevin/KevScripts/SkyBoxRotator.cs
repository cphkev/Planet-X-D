using UnityEngine;

public class SkyBoxRotator : MonoBehaviour
{
    public float rotationSpeed = 0.1f; // Speed of rotation (adjust as needed)

    void Update()
    {
        // Rotates the skybox's texture over time to simulate the rotation of stars or galaxies
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
