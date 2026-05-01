using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    // A slot to drag our Pause Menu panel into
    public GameObject pauseMenuObject;

    // A flag to keep track if we are paused
    private bool isPaused = false;

    public void OnPauseGame()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        Debug.Log("PauseGame function ran");

        // Show the cursor so we can click buttons
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //Turn on the PauseMenu
        pauseMenuObject.SetActive(true);
        isPaused = true;

        //Freeze time
        Time.timeScale = 0f;

        //lower ALL volume when paused
        AudioListener.volume = 0.3f;
    }

    void ResumeGame()
    {
        Debug.Log("ResumeGame function ran");
        // Hide the cursor 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Turn on the PauseMenu
        pauseMenuObject.SetActive(false);
        isPaused = false;

        //Freeze time
        Time.timeScale = 1f;

        //return ALL volume to normal when resumed
        AudioListener.volume = 1f;
    }

    /// <summary>
    /// This part is for the buttons
    /// </summary>
    public void OnPauseMenuResumeClicked()
    {
        ResumeGame();
    }
    public void OnPauseMenuHomeClicked()
    {
        //return ALL volume to normal when switching scenes
        AudioListener.volume = 1f;

        SceneManager.LoadScene("Title Screen");
    }
}
