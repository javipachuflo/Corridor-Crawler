using UnityEngine;
using UnityEngine.SceneManagement;

public class CorridorEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is tagged "Player"
        if (other.CompareTag("Player"))
        {
            // Call the Singleton instance of our generator
            if (CorridorGenerator.Instance != null)
            {
                CorridorGenerator.Instance.ResetAndExpandCorridor(other.gameObject);
            }
            else
            {
                SceneManager.LoadScene("Title Screen");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
