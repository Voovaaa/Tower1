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


    private void Awake()
    {
        saveLogic.InitializeDefaultProfileSaveData();
        profileData = saveLogic.getProfileSaveData();
        currentTile = Scripts.GetComponent<game>().getTileToSpawn();
        currentTile.GetComponent<tile>().setStatus("player");
    }


    public static void moveToTile(GameObject newTile)
    {
        currentTile.GetComponent<tile>().setStatus("wasHere");
        currentTile = newTile;
        currentTile.GetComponent<tile>().setStatus("player");
    }

    public static void gotXp(float xpGot)
    {
        float xp = float.Parse(saveLogic.getProfileSaveValue("xp"));
        xp += xpGot;
        if (xp > 1)
        {
            xp -= 1;
            lvlUp();
        }
        saveLogic.setProfileSaveValue("xp", xp.ToString());
    }
    public static void lvlUp()
    {
        int lvl = int.Parse(saveLogic.getProfileSaveValue("lvl"));
        lvl += 1;
        saveLogic.setProfileSaveValue("lvl", lvl.ToString());
    }
}
