using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    Rigidbody playerRigidbody;
    public Transform planet;
    public float speed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 2f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float jumpForce = 5f;
    public int maxJumps = 2;
    public float dashForce = 10f;
    public float dashDuration = 0.2f;

    private bool isGrounded;
    private bool isCrouching = false;
    private bool canDash = true;
    private bool isDashing = false;
    private int jumpCount = 0;
    private Vector3 originalScale;
    private float originalSpeed;
    private Vector3 moveVelocity;
    
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        originalSpeed = speed;
    }

    void Update()
    {
        CheckGround();
        HandleJump();
        HandleMovement();
        HandleCrouch();
        HandleDash();
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            Vector3 jumpDirection = (transform.position - planet.position).normalized;
            playerRigidbody.linearVelocity = Vector3.zero; 
            playerRigidbody.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }

    void HandleMovement()
    {
        if (isDashing) return; 

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        float targetSpeed = isSprinting ? sprintSpeed : (isCrouching ? crouchSpeed : speed);
        Vector3 moveDirection = (transform.right * x + transform.forward * z).normalized;
        
        moveVelocity = Vector3.Lerp(moveVelocity, moveDirection * targetSpeed, acceleration * Time.deltaTime);
        
        if (moveDirection == Vector3.zero)
        {
            moveVelocity = Vector3.Lerp(moveVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }
        
        playerRigidbody.MovePosition(transform.position + moveVelocity * Time.deltaTime);
    }

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isGrounded && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        Vector3 dashDirection = moveVelocity.normalized;
        playerRigidbody.linearVelocity = dashDirection * dashForce;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
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
            jumpCount = 0;
            canDash = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
