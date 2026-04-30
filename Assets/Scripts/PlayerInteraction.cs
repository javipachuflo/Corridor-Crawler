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
}