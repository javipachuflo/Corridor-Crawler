using UnityEngine;

public class CylinderInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacted with Cylinder");
    }
}
