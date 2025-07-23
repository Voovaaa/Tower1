using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public static GameObject currentTile;

    public static int currentFloorNumber;
    public static game.floor currentFloor;

    public GameObject Scripts;

    public static Dictionary<string, string> profileData;

    public static game.loot weapon;
    public static game.loot armor;

    public void Awake()
    {
        saveLogic.InitializeDefaultProfileSaveData();
        profileData = saveLogic.getProfileSaveData();
        currentTile = Scripts.GetComponent<game>().getTileToSpawn();
        currentTile.GetComponent<tile>().setStatus("player");
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
