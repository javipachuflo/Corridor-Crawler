using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // transform of the camera
    [SerializeField] private float interactDistance;
    [SerializeField] LayerMask interactLayerMask;

    private void OnInteract()
    {
        Debug.Log("E was pressed");
        // shoot a raycast and check if the object hit has a script with IInteractable. if yes, trigger the Interact() function.

        // 1. Define the ray starting at the camera and pointing forward
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        // Add this line to draw a red line in the Scene view that lasts for 2 seconds
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red, 2f);

        // 2. Shoot the raycast
        // out RaycastHit hitInfo stores the data of whatever the ray hits
        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactDistance, interactLayerMask))
        {
            // 3. Check if the object we hit has the IInteractable component attached
            // TryGetComponent is the most efficient way to check and grab the component at the same time
            if (hitInfo.collider.TryGetComponent<IInteractable>(out IInteractable interactableObject))
            {
                // 4. Trigger the interface method
                interactableObject.Interact();
                Debug.Log($"Successfully interacted with {hitInfo.collider.gameObject.name}");
            }
            else
            {
                Debug.Log($"Hit {hitInfo.collider.gameObject.name}, but it is not interactable.");
            }
        }
    }
}