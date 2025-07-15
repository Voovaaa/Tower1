using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //public static float hpCurrent;
    //public static float hpMax;
    //public static float armorValue;
    //public static float damageAverage;
    //public static int lvl;
    //public static float xp; // 0 - 1, needed xp doesnt change after lvlup

    //public static int positionX;
    //public static int positionY;

    public static Dictionary<string, string> profileData;


    private void Awake()
    {
        saveLogic.InitializeDefaultProfileSaveData();
        profileData = saveLogic.getProfileSaveData();
    }

    public static void gotXp(float xpGot)
    {
        float xp = float.Parse(saveLogic.getProfileSaveValue("xp"));
        xp += 0.3f;
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
