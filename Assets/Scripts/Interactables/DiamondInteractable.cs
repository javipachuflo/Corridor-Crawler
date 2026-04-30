using UnityEngine;

public class DiamondInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private string promptMessage = "Pick up Diamond";

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
