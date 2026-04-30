using UnityEngine;

public class LimitFPS : MonoBehaviour
{

    [SerializeField] private bool enabled = true;
    [SerializeField] private int FPSTarget = 60;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (enabled)
        {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = FPSTarget;
        }
    }

  
}
