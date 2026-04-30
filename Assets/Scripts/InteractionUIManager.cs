using UnityEngine;
using TMPro; // Required for TextMeshPro

public class InteractionUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject uiPanel; // The background/icon holding the text
    [SerializeField] private TextMeshProUGUI promptText; // The text component itself

    [Header("Settings")]
    [SerializeField] private Vector3 screenOffset = new Vector3(0, 30, 0); // Pushes the UI slightly above the object

    private Transform currentTarget;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        uiPanel.SetActive(false); // Hide the UI when the game starts
    }

    // Called by the Player script when looking at an object
    public void ShowUI(string prompt, Transform target)
    {
        promptText.text = prompt;
        currentTarget = target;
        uiPanel.SetActive(true);
    }

    // Called by the Player script when looking away
    public void HideUI()
    {
        currentTarget = null;
        uiPanel.SetActive(false);
    }

    // We use LateUpdate so the UI moves AFTER the player's camera has finished moving this frame
    private void LateUpdate()
    {
        if (currentTarget != null && uiPanel.activeSelf)
        {
            // Convert the 3D world position to 2D screen coordinates
            Vector3 screenPos = mainCam.WorldToScreenPoint(currentTarget.position);

            // screenPos.z > 0 means the object is in front of the camera. 
            // If it's behind us, we don't want the UI wrapping around the screen weirdly.
            if (screenPos.z > 0)
            {
                uiPanel.transform.position = screenPos + screenOffset;
            }
            else
            {
                // Push it way off screen if we somehow look away but the raycast hasn't cleared it yet
                uiPanel.transform.position = new Vector3(-1000, -1000, 0);
            }
        }
    }
}