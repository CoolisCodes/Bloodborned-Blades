using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    //public Vector3 startPosition = Vector3.zero; // Set this in Inspector

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    public Transform playerModel; // Assign in Inspector
    public Vector3 modelOffset = Vector3.zero; // Set in Inspector
    public Transform playerCamera; // Assign in Inspector (usually the main camera)
    public float mouseSensitivity = 100f;
    public Animator animator; // Assign in Inspector

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        // Set initial position
        // transform.position = startPosition;
    }

    void Update()
    {
        // Mouse look (horizontal only, rotate player)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        // Ground check
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Walking animation logic
        if (animator != null)
        {
            bool isWalking = (Mathf.Abs(x) > 0.01f || Mathf.Abs(z) > 0.01f);
            animator.SetBool("IsWalking", isWalking);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Apply model offset
        if (playerModel != null)
        {
            playerModel.position = transform.position + modelOffset;
        }
    }
}
