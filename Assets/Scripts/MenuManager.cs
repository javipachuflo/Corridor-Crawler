using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject audioSettings;


    public void OnPlayClicked() // triggers when "Play" is pressed in the Start Menu
    {
        unfreezeTime();
        SceneManager.LoadScene("Scene1");
    }

    public void ToggleAudioSettings()
    {

        audioSettings.SetActive(!audioSettings.activeSelf);
    }

    public void CloseAudioSettings()
    {
        audioSettings.SetActive(false);
    }

    public void PlayTutorial()
    {
        unfreezeTime();
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");

#if UNITY_EDITOR
        // This stops Play mode in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // This closes the application in a built standalone game
        Application.Quit();
#endif
    }

    // if the game is stopped when they left back to the home screen, it has to resume
    void unfreezeTime()
    {
        Time.timeScale = 1.0f;
    }
}
