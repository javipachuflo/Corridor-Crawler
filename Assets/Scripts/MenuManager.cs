using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject tutorialPrompt;

    public void OnPlayClicked() // triggers when "Play" is pressed in the Start Menu
    {
        tutorialPrompt.SetActive(true);
    }

    public void TestFunction()
    {
        Debug.Log("Scream!");
    }

    public void YesPlayTutorial()
    {
        tutorialPrompt.SetActive(false);
        unfreezeTime();
        SceneManager.LoadScene("Tutorial");

    }

    public void NoPlayTutorial()
    {
        tutorialPrompt.SetActive(false);
        unfreezeTime();
        SceneManager.LoadScene("Scene1");

    }

    // if the game is stopped when they left back to the home screen, it has to resume
    void unfreezeTime()
    {
        Time.timeScale = 1.0f;
    }


}
