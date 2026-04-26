using UnityEngine;
using UnityEngine.InputSystem;

public class E : MonoBehaviour
{
    private void OnInteract(InputValue value)
    {
        // Check if the button was just pressed down (ignores the "button released" event)
        if (value.isPressed)
        {
            Debug.Log("E");
        }
    }
}