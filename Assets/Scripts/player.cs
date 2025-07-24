using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
    public static GameObject currentTile;

    public static int currentFloorNumber;
    public static game.floor currentFloor;

    public static Dictionary<string, string> profileData;

    public static GameObject Scripts;

    public static game.loot weapon;
    public static game.loot armor;

    public static void initialize(GameObject scripts)
    {
        Scripts = scripts;
        saveLogic.InitializeDefaultProfileSaveData();
        profileData = saveLogic.getProfileSaveData();
        currentTile = scripts.GetComponent<game>().getTileToSpawn();
        currentTile.GetComponent<tile>().setStatus("player");

        weapon = saveLogic.getLootInstance(profileData["weapon"]);
        armor = saveLogic.getLootInstance(profileData["armor"]);
    }

    public static int calculateDamageValue()
    {
        return weapon.damageValue;
    }
    public static int calculateArmorValue()
    {
        return armor.armorValue;
    }

    public static void foundLoot(game.loot loot)
    {
        Scripts.GetComponent<game>().notify($"you found {loot.name}");
        if (loot.damageValue > weapon.damageValue)
        {
            weapon = loot;
        }
        if (loot.armorValue > armor.armorValue)
        {
            armor = loot;
        }
        currentFloor.lootTaken(loot);
    }

    public static void setProfileValue(string key, string value)
    {
        profileData[key] = value;
        saveLogic.setProfileSaveValue(key, value);
    }
    public static string getProfileValue(string key)
    {
        return profileData[key];
    }

    public static void moveToTile(GameObject newTile)
    {
        currentTile.GetComponent<tile>().setStatus("wasHere");
        currentTile = newTile;
        currentTile.GetComponent<tile>().setStatus("player");
    }

    
}
