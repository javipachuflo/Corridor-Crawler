using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // transform of the camera
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactLayerMask;

    [Header("UI Connection")]
    [SerializeField] private InteractionUIManager uiManager; // Drag your UI Manager here in the Inspector

    private IInteractable currentInteractable; // Caches what we are currently looking at

    private void Update()
    {
        CheckForInteractable();
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactDistance, interactLayerMask))
        {
            if (hitInfo.collider.TryGetComponent<IInteractable>(out IInteractable interactableObject))
            {
                // If we are looking at a NEW interactable, update the UI
                if (currentInteractable != interactableObject)
                {
                    currentInteractable = interactableObject;
                    uiManager.ShowUI(currentInteractable.GetPromptText(), currentInteractable.GetTransform());
                }
            }
            else
            {
                ClearInteractable(); // Hit something on the layer, but it lacks the script
            }
        }
        else
        {
            ClearInteractable(); // Hit nothing, we are looking at empty space
        }
    }

    private void ClearInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable = null;
            uiManager.HideUI();
        }
    }

    private void OnInteract()
    {
        // No need to raycast again! We already know what we are looking at from Update().
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
            Debug.Log("Successfully interacted!");
        }
    }

    //private void OnInteract()
    //{
    //    Debug.Log("E was pressed");
    //    // shoot a raycast and check if the object hit has a script with IInteractable. if yes, trigger the Interact() function.

    //    // 1. Define the ray starting at the camera and pointing forward
    //    Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

    //    // Add this line to draw a red line in the Scene view that lasts for 2 seconds
    //    Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red, 2f);

    //    // 2. Shoot the raycast
    //    // out RaycastHit hitInfo stores the data of whatever the ray hits
    //    if (Physics.Raycast(ray, out RaycastHit hitInfo, interactDistance, interactLayerMask))
    //    {
    //        // 3. Check if the object we hit has the IInteractable component attached
    //        // TryGetComponent is the most efficient way to check and grab the component at the same time
    //        if (hitInfo.collider.TryGetComponent<IInteractable>(out IInteractable interactableObject))
    //        {
    //            // 4. Trigger the interface method
    //            interactableObject.Interact();
    //            Debug.Log($"Successfully interacted with {hitInfo.collider.gameObject.name}");
    //        }
    //        else
    //        {
    //            Debug.Log($"Hit {hitInfo.collider.gameObject.name}, but it is not interactable.");
    //        }
    //    }
    //}
}