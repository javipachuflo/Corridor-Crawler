using System.Collections;
using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsLayers;

public class LeverInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private string promptMessage = "Pull Lever";
    [SerializeField] private Animator animator;
    [SerializeField] Transform trapdoorTransform;
    [SerializeField] Transform trapdoorStartPosition;
    [SerializeField] Transform trapdoorEndPosition;

    [SerializeField] private float trapdoorMovementDuration = 5f;
    [SerializeField] private AnimationCurve animationCurve;

    public void Interact()
    {
        setLayerMask("Default");

        Debug.Log("Interacted with " + promptMessage);
        if (animator != null)
        {
            animator.SetTrigger("OnPull");
        }
        else
        {
            Debug.Log("Animator Component is Missing!");
        }

        StartCoroutine(MoveTrapdoor());
    }

    private IEnumerator MoveTrapdoor()
    {
        float timeElapsed = 0f;

        while (timeElapsed < trapdoorMovementDuration)
        {
            float t = timeElapsed / trapdoorMovementDuration;
            trapdoorTransform.position = Vector3.Lerp(trapdoorStartPosition.position, trapdoorEndPosition.position, animationCurve.Evaluate(t));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private void setLayerMask(string layerMaskName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerMaskName);
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
