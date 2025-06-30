using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 5f; // Speed of the player movement
    [SerializeField]
    private float jumpForce = 15f; // Force applied when the player jumps
    private PlayerInput playerInput; // Reference to the PlayerInput component
    private InputAction move;
    private InputAction jump;
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Animator animator; // Reference to the Animator component (if needed for animations)    
    // Thêm lên đầu class
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to this GameObject
        playerInput = GetComponent<PlayerInput>(); // Get the PlayerInput component attached to this GameObject
        animator = GetComponent<Animator>(); // Get the Animator component attached to this GameObject (if needed for animations)
        move = playerInput.actions["Move"]; // Get the "Move" action from the PlayerInput component
        jump = playerInput.actions["Jump"]; // Get the "Move" action from the PlayerInput component
        jump.performed += OnJumpEvent;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OnMovement(); // Call the HandleMovement method to process player input
        CheckGround(); // Check if the player is grounded
        HandleAnimation(); // Call the HandleAnimation method to update animations based on player state
    }

    void OnMovement()
    {
        Vector2 moveInput = move.ReadValue<Vector2>(); // Read the movement input from the PlayerInput component
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y); // Apply the horizontal movement while maintaining the current vertical velocity
        if (moveInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1); // Flip the player sprite based on the direction of movement
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }


    void OnJumpEvent(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {
            rb.linearVelocity = Vector2.up * jumpForce;
        }
    }

    void HandleAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f; // Check if the player is moving
        bool isJumping = !isGrounded; // Check if the player is jumping
        animator.SetBool("isRunning", isRunning); // Set the running animation based on movement
        animator.SetBool("isJumping", isJumping); // Set the jumping animation based
    }
}
