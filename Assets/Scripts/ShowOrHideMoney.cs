using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowOrHideMoney : MonoBehaviour
{
    // This creates a static reference to the ONE true instance
    public static ShowOrHideMoney Instance { get; private set; }

    [Header("UI References")]
    [Tooltip("Drag the child Canvas or UI Panel here. Do NOT drag this root object!")]
    public GameObject uiContent;

    [Header("Visibility Settings")]
    [Tooltip("Type the exact names of the scenes where this UI should NOT appear.")]
    public List<string> scenesWhereUIIsHidden = new List<string>();

    void Awake()
    {
        // The Singleton Check 
        // If an instance already exists, and it's not THIS specific object...
        if (Instance != null && Instance != this)
        {
            // Destroy this duplicate object immediately
            Destroy(gameObject);

            // Stop running the rest of the Awake method so we don't do any more setup
            return;
        }

        // If we made it this far, no other instance exists. 
        // Claim the title of the one true Instance.
        Instance = this;

        // Standard DontDestroyOnLoad setup
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        // Subscribe to the scene load event when this script turns on
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe when destroyed to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This function automatically runs every time ANY scene finishes loading
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the newly loaded scene's name is in our 'hide' list
        if (scenesWhereUIIsHidden.Contains(scene.name))
        {
            uiContent.SetActive(false);
        }
        else
        {
            uiContent.SetActive(true);
        }
    }
}