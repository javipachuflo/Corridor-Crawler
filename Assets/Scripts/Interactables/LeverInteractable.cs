using System.Collections;
using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsLayers;

public class LeverInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private string promptMessage = "Pull Lever";

    [SerializeField] private int requiredMoney = 500; // can be changed in the inspector

    [SerializeField] private Animator animator;
    [SerializeField] private float trapdoorMovementDuration = 5f;
    [SerializeField] private AnimationCurve animationCurve;

    [SerializeField] Transform trapdoorTransform;
    [SerializeField] Transform trapdoorStartPosition;
    [SerializeField] Transform trapdoorEndPosition;
    [SerializeField] AudioSource trapdoorStoneSliding;

    [SerializeField] private AudioSource pullingLeverAudio;
    [SerializeField] private AudioSource boughtSomethingSound;

    public void Interact()
    {
        if (MoneyManager.Instance.SpendMoney(requiredMoney))
        {
            setLayerMask("Default");

            Debug.Log("Interacted with " + promptMessage);
            if (animator != null)
            {
                animator.SetTrigger("OnPull");
                pullingLeverAudio.Play();
                boughtSomethingSound.Play();
                trapdoorStoneSliding.Play();
            }
            else
            {
                Debug.Log("Animator Component is Missing!");
            }

            StartCoroutine(MoveTrapdoor());

        } else
        {
            Debug.Log("Not enough money!");
        }
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
