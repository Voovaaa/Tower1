using JetBrains.Annotations;
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
    public static Dictionary<string, game.loot> nameNloot;

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
        }
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
            "weapon;" +
            "armorValue;" +
            "damage;" + //5
            "lvl;" +
            "xp;" +
            "skillPoints;" +
            "foodLeft;" +
            "villagersLeft;" + // 10
            "timeLeft;" +
            "foodCollected;";
        string saveValuesString = "10;" +
            "underwear;" +
            "fists;" +
            "0;" +
            "0;" + //5
            "0;" +
            "0;" +
            "0;" +
            "23;" +
            "22;" + // 10
            "15;" +
            "0;";

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




    public static game.loot getLootInstance(string name)
    {
        return nameNloot[name];
    }
    public static string getInventorySaveValue(string equipment)
    {
        return getProfileSaveValue(equipment);
    }
    public static void setInventorySaveValue(string equipment, string lootName) // equipment - armor/weapon
    {
        setProfileSaveValue(equipment, lootName);
    }
    public static void initializeAllLoot()
    {
        allLoot = new List<game.loot>();
        floorNloot = new Dictionary<int, List<game.loot>>();
        nameNloot = new Dictionary<string, game.loot>();

        game.loot fists = new game.loot("fists", 1, 0);
        game.loot underwear = new game.loot("underwear", 0, 0);
        allLoot.Add(fists);
        allLoot.Add(underwear);
        nameNloot["fists"] = fists;
        nameNloot["underwear"] = underwear;

        floorNloot[2] = new List<game.loot>();
        game.loot woodenClub = new game.loot("wooden club", 2, 0);
        addLootToData(2, woodenClub);
        game.loot tShirt = new game.loot("tshirt and shorts", 0, 1);
        addLootToData(2, tShirt);

        static void addLootToData(int floorNumber, game.loot loot)
        {
            allLoot.Add(loot);
            floorNloot[floorNumber].Add(loot);
            nameNloot[loot.name] = loot;
        }
    }





    


}
