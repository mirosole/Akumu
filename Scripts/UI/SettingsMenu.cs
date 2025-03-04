using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel; 
    [SerializeField] private Slider volumeSlider; 
    [SerializeField] private Slider mouseSensitivitySlider; 

    public static float MouseSensitivity = 1.0f;

    private void Start()
    {
        settingsPanel.SetActive(false);

        volumeSlider.onValueChanged.AddListener(SetVolume);
        volumeSlider.value = AudioListener.volume;
        mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
        mouseSensitivitySlider.value = MouseSensitivity; 
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        MouseSensitivity = sensitivity;
    }
}