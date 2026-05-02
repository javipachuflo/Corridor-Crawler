using UnityEngine;

public class TitleScreenCameraBob : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("How fast the overall bobbing effect is.")]
    [Range(0.1f, 5f)]
    public float bobSpeed = 1f;

    [Header("Position Bobbing")]
    [Tooltip("How far the camera moves on the X, Y, and Z axes. Keep these values very low for a subtle effect.")]
    public Vector3 positionAmplitude = new Vector3(0.1f, 0.2f, 0f);

    [Header("Rotation Bobbing")]
    [Tooltip("How much the camera tilts. A tiny bit of rotation makes it feel much more organic.")]
    public Vector3 rotationAmplitude = new Vector3(0.5f, 0.5f, 0f);

    // To store the original state of the camera
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        // Remember where the camera was placed in the editor
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        // Generate our base waves using time
        // We use both Sin and Cos so the axes don't move in perfect sync (which looks robotic)
        float sinWave = Mathf.Sin(Time.time * bobSpeed);
        float cosWave = Mathf.Cos(Time.time * bobSpeed * 0.8f); // Slightly offset speed for variation

        // 1. Calculate new position
        Vector3 positionOffset = new Vector3(
            positionAmplitude.x * cosWave,
            positionAmplitude.y * sinWave,
            positionAmplitude.z * sinWave
        );
        transform.position = startPosition + positionOffset;

        // 2. Calculate new rotation
        Vector3 rotationOffset = new Vector3(
            rotationAmplitude.x * sinWave,
            rotationAmplitude.y * cosWave,
            rotationAmplitude.z * sinWave
        );

        // Apply the rotation offset to the original rotation
        transform.rotation = startRotation * Quaternion.Euler(rotationOffset);
    }
}