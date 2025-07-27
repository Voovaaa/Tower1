using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class settings : MonoBehaviour
{
    List<string> resolutionOptions;

    public static bool isFullscreen;
    public static float volumeValue;
    public static int resolutionOption;
    public static Dictionary<string, string> settingsSave;

    
    private void Start()
    {
        makeResolutionsDropDown(); //!!! <- ????
    }

    public static void fullscreenToggle()
    {
        isFullscreen = !isFullscreen;
        Screen.fullScreen = isFullscreen;
        saveLogic.setSettingsSave(isFullscreen, volumeValue, resolutionOption);
    }
    public static void volumeSlider(float sliderValue)
    {
        volumeValue = sliderValue;
        mainMenu.audioMixer.SetFloat("master volume", sliderValue);
        saveLogic.setSettingsSave(isFullscreen, volumeValue, resolutionOption);
    }
    public static void resolutionsDropDown(int resolutionOptionTo)
    {
        settings.resolutionOption = resolutionOptionTo;
        Screen.SetResolution(mainMenu.resolutions[resolutionOption].width, mainMenu.resolutions[resolutionOption].height, isFullscreen);
        saveLogic.setSettingsSave(isFullscreen, volumeValue, resolutionOption);
        Debug.Log($"{settings.settingsSave["isFullscreen"]}, {settings.settingsSave["volume"]}, {settings.settingsSave["resolutionOption"]}DROPDOWN");
    }

    void makeResolutionsDropDown()
    {
        TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();
        resolutionOptions = new List<string>();
        if (dropdown)
        {
            for (int i = 0; i < mainMenu.resolutions.Length; i++)
            {
                resolutionOptions.Add($"{mainMenu.resolutions[i].width}x{mainMenu.resolutions[i].height}");
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(resolutionOptions);
        }
    }
}
