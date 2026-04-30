using UnityEngine;

public class CylinderInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private string promptMessage = "Interact with Cylinder";

    public void Interact()
    {
        Debug.Log("Interacted with " + promptMessage);
    }
    public string GetPromptText()
    {
        return promptMessage;
    }

    public Transform GetTransform()
    {
        // Returning this object's transform allows the UI to track it
        return transform;
    }
}
