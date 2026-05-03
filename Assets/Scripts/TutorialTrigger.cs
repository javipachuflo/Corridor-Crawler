using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [Header("Trigger Settings")]
    [Tooltip("Which slide should this show? (0 = first slide, 1 = second slide, etc.)")]
    public int slideIndexToTrigger;

    [Tooltip("The tag required to activate this trigger.")]
    public string playerTag = "Player";

    private TutorialManager tutorialManager;
    private Collider triggerCollider;

    private void Awake()
    {
        // Grab the collider once at the start to save performance
        triggerCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        // Automatically find the Tutorial Manager in the scene
        tutorialManager = FindAnyObjectByType<TutorialManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the thing entering the trigger is the Player
        if (other.CompareTag(playerTag))
        {
            if (tutorialManager != null)
            {
                // Tell the manager to show the assigned slide
                tutorialManager.ShowSlide(slideIndexToTrigger);
            }

            SetTriggerActive(false);
        }
    }

    // New helper method for the Manager to use
    public void SetTriggerActive(bool isActive)
    {
        if (triggerCollider != null)
        {
            triggerCollider.enabled = isActive;
        }
    }

}