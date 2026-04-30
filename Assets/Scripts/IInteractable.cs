using UnityEngine;

public interface IInteractable
{
    void Interact();
    string GetPromptText();
    Transform GetTransform();
}
