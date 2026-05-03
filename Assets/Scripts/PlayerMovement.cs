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

    [Header("Audio")]
    [SerializeField] private AudioSource footstepsLoop;
    [SerializeField] private AudioSource jumpingSound;
    [SerializeField] private AudioSource landingSound;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool wasGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        wasGrounded = isGrounded;
        
        // 1. Industry Standard Ground Check: Creates an invisible sphere to check for the Ground layer
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(!wasGrounded && isGrounded)
        {
            HandleLanding();
        }

        // 2. Read the Vector2 input from WASD continuously
        moveInput = moveAction.action.ReadValue<Vector2>();

        Debug.Log("Input is: " + moveInput);

        HandleFootsteps();
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

    private void OnJump()
    {
        if (isGrounded)
        {
            // Reset the Y velocity before adding force to ensure jump heights are always exactly the same
            jumpingSound.Play();
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleLanding()
    {
        landingSound.Play();
    }

    private void HandleFootsteps()
    {
        // Use sqrMagnitude > 0.01f instead of checking exactly against 0. 
        // This acts as a deadzone check and prevents controller stick drift from playing audio.
        bool isMoving = moveInput.sqrMagnitude > 0.01f;

        if (isMoving && isGrounded)
        {
            // Only play if it isn't already playing, otherwise it will restart every frame and sound like a buzz
            if (!footstepsLoop.isPlaying)
            {
                footstepsLoop.Play();
            }
        }
        else
        {
            // Stop the audio if the player stops moving or leaves the ground
            if (footstepsLoop.isPlaying)
            {
                footstepsLoop.Stop();
            }
        }
    }
}