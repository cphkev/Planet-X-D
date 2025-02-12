using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = -9.8f;
    public float rotationSmoothness = 5f; // Adjust this for smoother rotation

    public void Attract(Rigidbody body)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 localUp = body.transform.up;

        // Apply downwards gravity as a consistent acceleration
        body.AddForce(gravityUp * gravity, ForceMode.Acceleration);

        // Smoothly rotate towards planet's up direction
        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
    }
}
