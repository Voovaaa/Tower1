using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{

    Dictionary<string, string> settingsSaveData;

    public AudioMixer audioMixerInstance;
    public Toggle fullscreenToggle;
    public Slider volumeSlider;
    public TMP_Dropdown resolutionsDropDown;

    static public Resolution[] resolutions;
    static public AudioMixer audioMixer;
    private void Awake()
    {
        settingsInitialization();
    }
    private void Start()
    {
        if (!settings.isFullscreen)
        {
            settings.isFullscreen = true;
            fullscreenToggle.isOn = false;
        }

        volumeSlider.value = settings.volumeValue;

        resolutionsDropDown.value = settings.resolutionOption;
    }
    void settingsInitialization()
    {
        resolutions = Screen.resolutions;
        audioMixer = audioMixerInstance;
        settings.settingsSave = saveLogic.getSettingsSave();
        settings.isFullscreen = bool.Parse(settings.settingsSave["isFullscreen"]);
        settings.volumeValue = float.Parse(settings.settingsSave["volume"]);
        settings.resolutionOption = int.Parse(settings.settingsSave["resolutionOption"]);
    }
    
}

