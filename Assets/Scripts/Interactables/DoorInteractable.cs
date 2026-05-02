using System.Collections;
using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsLayers;

public class DoorInteractable : MonoBehaviour, IInteractable
{

    private string promptMessage;
    private bool isOpen = false;
    private bool isOpening = false;

    [SerializeField] Transform doorTransform;
    [SerializeField] Transform doorStartPosition;
    [SerializeField] Transform doorEndPosition;

    [SerializeField] private float doorMovementDuration = 2f;
    [SerializeField] private AnimationCurve animationCurve;

    [SerializeField] AudioSource woodenSlidingDoorOpeningAudio;
    [SerializeField] AudioSource woodenSlidingDoorClosingAudio;

    public void Interact()
    {
        Debug.Log("Interacted with " + promptMessage);

        if (!isOpening) // only start Coroutine if the door is not currently opening
        {
            StartCoroutine(MoveDoor());
        }

    }

    private IEnumerator MoveDoor()
    {
        isOpening = true; // set isOpening to true to prevent spamming
        setLayerMask("Default");
        float timeElapsed = 0f;

        if (!isOpen)
        {
            woodenSlidingDoorOpeningAudio.Play();
        }
        else
        {
            woodenSlidingDoorClosingAudio.Play();
        }

        while (timeElapsed < doorMovementDuration)
        {
            float t = timeElapsed / doorMovementDuration;
            if (!isOpen) // animation to open the door if it isn't already open
            {
                doorTransform.position = Vector3.Lerp(doorStartPosition.position, doorEndPosition.position, animationCurve.Evaluate(t));

            } else if (isOpen) // animation to close door is it is open
            {
                doorTransform.position = Vector3.Lerp(doorEndPosition.position, doorStartPosition.position, animationCurve.Evaluate(t));
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        isOpening = false; // set isOpening to false to allow closing/opening again
        isOpen = !isOpen;
        setLayerMask("Interactable");
    }

    private void setLayerMask(string layerMaskName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerMaskName);
    }


    public string GetPromptText()
    {
        if (!isOpen)
        {
            promptMessage = "Open Door";
        }
        else
        {
            promptMessage = "Close Door";
        }
        return promptMessage;
    }

    public Transform GetTransform()
    {
        // Returning this object's transform allows the UI to track it
        return transform;
    }
}
