using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask; // Assign your "Ground" layer here in the inspector

    [Header("Input References")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();

        // Subscribe to the jump button press
        jumpAction.action.performed += OnJump;
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();

        // Unsubscribe to prevent memory leaks
        jumpAction.action.performed -= OnJump;
    }

    private void Update()
    {
        // 1. Industry Standard Ground Check: Creates an invisible sphere to check for the Ground layer
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // 2. Read the Vector2 input from WASD continuously
        moveInput = moveAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // 3. Apply movement. We use FixedUpdate because we are manipulating a Rigidbody.
        // We move relative to the player's local transform (where they are facing).
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
        Vector3 targetVelocity = moveDirection * moveSpeed;

        // Keep the current Y velocity so gravity still works naturally
        // Note: Unity 6 uses linearVelocity instead of the old 'velocity' property
        targetVelocity.y = rb.linearVelocity.y;

        rb.linearVelocity = targetVelocity;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            // Reset the Y velocity before adding force to ensure jump heights are always exactly the same
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}