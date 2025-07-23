using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class saveLogic : MonoBehaviour
{

    public static string[] saveKeys;
    public static string[] defaultSaveValues;
    public static Dictionary<string, string> defaultProfileSaveData;

    public static Dictionary<string, string> currentProfileSaveData;

    public static List<game.loot> allLoot;
    public static Dictionary<int, List<game.loot>> floorNloot;

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


    public static void setProfileSave(Dictionary<string, string> profileData)
    {
        string profileName = PlayerPrefs.GetString("currentProfileName");
        foreach (KeyValuePair<string, string> dataPair in profileData)
        {
            setProfileSaveValue(dataPair.Key, dataPair.Value);
            //PlayerPrefs.SetString($"{profileName} {dataPair.Key}", dataPair.Value);
        }
        //PlayerPrefs.Save();
    }
    public static Dictionary<string, string> getProfileSaveData()
    {
        string profileName = PlayerPrefs.GetString("currentProfileName");
        Dictionary<string, string> profileSaveData = new Dictionary<string, string>();
        if (PlayerPrefs.HasKey($"{profileName} {saveKeys[saveKeys.Length - 1]}"))
        {
            for (int i = 0; i < saveKeys.Length; i++)
            {
                profileSaveData.Add(saveKeys[i], getProfileSaveValue(saveKeys[i]));
            }
        }
        else
        {
            createProfile(profileName);
            profileSaveData = getProfileSaveData();
        }
        return profileSaveData;
    }
    public static void createProfile(string profileName) { setProfileSave(defaultProfileSaveData); }
    public static void InitializeDefaultProfileSaveData()
    {
        string saveKeysString = "hpMax;" +
            "armor;" +
            "damage;";
        string saveValuesString = "10;" +
            "0;" +
            "1;";

        saveKeys = saveKeysString.Split(";");
        defaultSaveValues = saveValuesString.Split(";");

        defaultProfileSaveData = new Dictionary<string, string>();
        for (int i = 0; i < saveKeys.Length; i++)
        {
            defaultProfileSaveData.Add(saveKeys[i], defaultSaveValues[i]);
        }


    }
    public static void setProfileSaveValue(string key, string value)
    {
        PlayerPrefs.SetString($"{PlayerPrefs.GetString("currentProfileName")} {key}", value);
        PlayerPrefs.Save();
    }
    public static string getProfileSaveValue(string key)
    {
        return PlayerPrefs.GetString($"{PlayerPrefs.GetString("currentProfileName")} {key}");
    }

    public static void setFloorSaveValue(int floorNumber, string key, string value)
    {
        setProfileSaveValue($"floor {floorNumber} {key}", value);
    }
    public static string getFloorSaveValue(int floorNumber, string key)
    {
        return getProfileSaveValue($"floor {floorNumber} {key}");
    }




    public static List<game.loot> getAllLoot ()
    {
        return null;
    }
    public static game.loot getLootInstance(string name)
    {
        return null;
    }
    public static void lootFound()
    {
        //saveLogic.setInventorySaveValue(floorNumber, $"LOOT{lootInstance.name}", "true");
    }
    public static void setInventorySaveValue(string lootName, bool isFound)
    {
        setProfileSaveValue($"LOOT{lootName}", isFound.ToString());
    }
    public static void initializeAllLoot()
    {
        allLoot = new List<game.loot>();
        floorNloot = new Dictionary<int, List<game.loot>>();

        floorNloot[2] = new List<game.loot>();
        game.loot woodenClub = new game.loot("woodenClub", 1, 1);
        allLoot.Add(woodenClub);
        floorNloot[2].Add(woodenClub);
        game.loot tShirt = new game.loot("tShirt", 0, 1);
        allLoot.Add(tShirt);
        floorNloot[2].Add(tShirt);
    }





    


}
