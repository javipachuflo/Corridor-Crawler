using UnityEngine;

public class ButtonInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private string promptMessage = "Press Button";
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        Debug.Log("Interacted with " + promptMessage);
        if (animator != null) {
            animator.SetTrigger("OnPress");
        }
        else
        {
            Debug.Log("Animator Component is Missing!");
        }

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
