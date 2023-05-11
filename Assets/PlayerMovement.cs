using UnityEngine;

public class FirstPersonController : MonoBehaviour
{

    public float movementSpeed = 5f;   // Player movement speed
    public float jumpForce = 7f;       // Jump force
    public float crouchSpeed = 2f;     // Speed multiplier when crouching

    private bool isCrouching = false;  // Whether the player is crouching
    private Rigidbody rb;              // Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move forward/backward
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * movementSpeed * Time.deltaTime);

        // Strafe left/right
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * movementSpeed * Time.deltaTime);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && !isCrouching)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Crouch
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isCrouching = true;
            transform.localScale = new Vector3(1, 0.5f, 1);
            movementSpeed /= crouchSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isCrouching = false;
            transform.localScale = new Vector3(1, 1, 1);
            movementSpeed *= crouchSpeed;
        }
    }
}
