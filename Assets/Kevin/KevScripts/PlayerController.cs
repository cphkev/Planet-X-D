using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    public Transform planet; // Reference to the planet
    public float speed = 5f;
    public float crouchSpeed = 2f;
    public float jumpForce = 5f;
    public int maxJumps = 2; // Allow double jump

    private bool isGrounded;
    private bool isCrouching = false;
    private int jumpCount = 0; // Track jumps
    private Vector3 originalScale;
    private float originalSpeed;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        originalSpeed = speed;
    }

    void Update()
    {
        CheckGround();
        Jump();
        Move();
        HandleCrouch();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            Vector3 jumpDirection = (transform.position - planet.position).normalized;
            playerRigidbody.linearVelocity = Vector3.zero; // Reset velocity for better control
            playerRigidbody.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);

            jumpCount++; // Increase jump count
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        playerRigidbody.MovePosition(transform.position + move * speed * Time.deltaTime);
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
        {
            if (!isCrouching)
            {
                Crouch();
            }
            else
            {
                StandUp();
            }
        }
    }

    void Crouch()
    {
        isCrouching = true;
        transform.localScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z);
        transform.position -= new Vector3(0, originalScale.y * 0.25f, 0);
        speed = crouchSpeed;
    }

    void StandUp()
    {
        isCrouching = false;
        transform.localScale = originalScale;
        transform.position += new Vector3(0, originalScale.y * 0.25f, 0);
        speed = originalSpeed;
    }

    void CheckGround()
    {
        RaycastHit hit;
        Vector3 gravityDirection = (planet.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, gravityDirection, out hit, 1.1f))
        {
            isGrounded = true;
            jumpCount = 0; // Reset jump count when grounded
        }
        else
        {
            isGrounded = false;
        }
    }
}
