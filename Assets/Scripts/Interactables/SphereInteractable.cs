using UnityEngine;

public class SphereInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacted with Sphere");
    }
}
