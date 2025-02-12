using UnityEngine;

public class SunRotation : MonoBehaviour
{
    public Transform planet;  // Reference to the planet or world object
    public float rotationSpeed = 10f;  // Adjust this for the speed of rotation

    // Update is called once per frame
    void Update()
    {
        // Rotate the sun around the planet using the center of the planet as the pivot point
        transform.RotateAround(planet.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
