using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveLogic : MonoBehaviour
{

    public static string[] saveKeys;
    public static string[] defaultSaveValues;
    public static Dictionary<string, string> defaultProfileSaveData;

    public static string currentProfileSaveName;
    public static Dictionary<string, string> currentProfileSaveData;

    public static void setSettingsSave(bool isFullscreen, float volume, int resolutionOption)
    {
        PlayerPrefs.SetString("isFullscreen", isFullscreen.ToString());
        PlayerPrefs.SetString("volume", volume.ToString());
        PlayerPrefs.SetString("resolutionOption", resolutionOption.ToString());
        PlayerPrefs.Save();
        settings.settingsSave = new Dictionary<string, string>{ { "isFullscreen", isFullscreen.ToString()}, { "volume",  volume.ToString()}, { "resolutionOption",  resolutionOption.ToString()} };
    }
    public static Dictionary<string, string> getSettingsSave()
    {
        Dictionary<string, string> settingsSave = new Dictionary<string, string>();
        if (PlayerPrefs.HasKey("isFullscreen"))
        {
            settingsSave.Add("isFullscreen", PlayerPrefs.GetString("isFullscreen"));
            settingsSave.Add("volume", PlayerPrefs.GetString("volume"));
            settingsSave.Add("resolutionOption", PlayerPrefs.GetString("resolutionOption"));
        }
        else
        {
            settingsSave.Add("isFullscreen", "true");
            settingsSave.Add("volume", "-10");
            settingsSave.Add("resolutionOption", "1");
            setSettingsSave(true, -10f, 1);
        }
        return settingsSave;
    }
    public static void setProfileSave(string profileName, Dictionary<string, string> profileData)
    {
        foreach (KeyValuePair<string, string> dataPair in profileData)
        {
            PlayerPrefs.SetString($"{profileName} {dataPair.Key}", dataPair.Value);
        }
        PlayerPrefs.Save();
    }
    public static Dictionary<string, string> getProfileSaveData(string profileName)
    {
        Dictionary<string, string> profileSaveData = new Dictionary<string, string>();
        if (PlayerPrefs.HasKey($"{profileName} {saveKeys[0]}"))
        {
            for (int i = 0; i < saveKeys.Length; i++)
            {
                profileSaveData.Add(saveKeys[i], PlayerPrefs.GetString($"{profileName} {saveKeys[i]}"));
            }
        }
        else
        {
            createProfile(profileName);
            profileSaveData = getProfileSaveData(profileName);
        }
        return profileSaveData;
    }
    public static void createProfile(string profileName) { setProfileSave(profileName, defaultProfileSaveData); }
    public static void InitializeDefaultProfileSaveData()
    {
        string saveKeysString = "huy jopa text"; // 1
        string saveValuesString = "a b qwe"; // 1
        saveKeys = saveKeysString.Split(" ");
        defaultSaveValues = saveValuesString.Split(" ");
        defaultProfileSaveData = new Dictionary<string, string>();
        for (int i = 0; i < saveKeys.Length; i++)
        {
            defaultProfileSaveData.Add(saveKeys[i], defaultSaveValues[i]);
        }
    }
}
