using UnityEngine;

public class PlayerLookSync : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Drag your Main Camera here, NOT the Cinemachine Camera")]
    public Transform mainCameraTransform;

    private void Awake()
    {
        // Automatically find the main camera if you forget to assign it
        if (mainCameraTransform == null)
        {
            mainCameraTransform = Camera.main.transform;
        }

        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    private void Update()
    {
        // Only rotate the player's Y axis (left and right) to match the camera.
        // We do not want the player capsule leaning forward or backward!
        transform.rotation = Quaternion.Euler(0f, mainCameraTransform.eulerAngles.y, 0f);
    }
}