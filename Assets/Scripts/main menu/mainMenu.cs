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

    public static Resolution[] resolutions;
    public static AudioMixer audioMixer;
    private void Awake()
    {
        settingsInitializationOnAwake();
    }


    private void Start()
    {
        settingsInitializationOnStart();
    }
    














    void settingsInitializationOnAwake()
    {
        resolutions = Screen.resolutions;
        audioMixer = audioMixerInstance;
        settings.settingsSave = saveLogic.getSettingsSave();
        settings.isFullscreen = bool.Parse(settings.settingsSave["isFullscreen"]);
        settings.volumeValue = float.Parse(settings.settingsSave["volume"]);
        settings.resolutionOption = int.Parse(settings.settingsSave["resolutionOption"]);
    }
    void settingsInitializationOnStart()
    {
        if (!settings.isFullscreen)
        {
            settings.isFullscreen = true;
            fullscreenToggle.isOn = false;
        }

        volumeSlider.value = settings.volumeValue;

        resolutionsDropDown.value = settings.resolutionOption;
    }
}

