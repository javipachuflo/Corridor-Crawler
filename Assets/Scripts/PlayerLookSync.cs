using UnityEngine;

// This ensures we always have a Rigidbody to reference
[RequireComponent(typeof(Rigidbody))]
public class PlayerLookSync : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Drag your Main Camera here, NOT the Cinemachine Camera")]
    public Transform mainCameraTransform;

    private Rigidbody rb;

    private void Awake()
    {
        // Automatically find the main camera if you forget to assign it
        if (mainCameraTransform == null)
        {
            mainCameraTransform = Camera.main.transform;
        }

        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        // 1. Calculate the target rotation based on the camera's Y axis
        Quaternion targetRotation = Quaternion.Euler(0f, mainCameraTransform.eulerAngles.y, 0f);

        // 2. Apply it using MoveRotation. This plays perfectly with Rigidbody Interpolation!
        rb.MoveRotation(targetRotation);
    }
}