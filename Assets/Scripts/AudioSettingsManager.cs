using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    [Header("Mixer Reference")]
    public AudioMixer gameAudioMixer;

    // These methods will be called by your sliders
    public void SetMasterVolume(float sliderValue)
    {
        // Mathf.Log10 converts the linear slider value to a logarithmic curve
        gameAudioMixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicVolume(float sliderValue)
    {
        gameAudioMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        gameAudioMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetUIVolume(float sliderValue)
    {
        gameAudioMixer.SetFloat("UIVol", Mathf.Log10(sliderValue) * 20);
    }
}