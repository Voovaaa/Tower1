using System.Collections.Generic;
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

    
    public static void initialize()
    {
        Scripts = game.scripts;
        saveLogic.InitializeDefaultProfileSaveData();
        profileData = saveLogic.getProfileSaveData();
        currentTile = Scripts.GetComponent<game>().getTileToSpawn();
        moveToTile(currentTile);

        weapon = saveLogic.getLootInstance(profileData["weapon"]);
        armor = saveLogic.getLootInstance(profileData["armor"]);
    }

    public static int calculateDamageValue()
    {
        return weapon.damageValue + int.Parse(profileData["damage"]);
    }
    public static int calculateArmorValue()
    {
        return armor.armorValue + int.Parse(profileData["armorValue"]);
    }

    public static void foundLoot(game.loot loot)
    {
        Scripts.GetComponent<game>().notify($"you've found {loot.name}");
        if (loot.damageValue > weapon.damageValue)
        {
            weapon = loot;
            setProfileValue("weapon", weapon.name);
        }
        if (loot.armorValue > armor.armorValue)
        {
            armor = loot;
            setProfileValue("armor", armor.name);
        }
        currentFloor.lootTaken(loot);
    }

    public static void gotXp(float experience)
    {
        float xp = float.Parse(profileData["xp"]) + experience;
        if (xp > 1)
        {
            lvlUp();
            xp -= 1;
        }
        setProfileValue("xp", xp.ToString());
    }
    public static void lvlUp()
    {
        Scripts.GetComponent<game>().notify("you've lvlupped");
        int skillPoints = int.Parse(profileData["skillPoints"]) + 1;
        int lvl = int.Parse(profileData["lvl"]) + 1;
        setProfileValue("lvl", lvl.ToString());
        setProfileValue("skillPoints", skillPoints.ToString());
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
        tile tile = currentTile.GetComponent<tile>();
        tile.setStatus("wasHere", currentFloorNumber, tile.tileNumber);
        currentTile = newTile;
        tile newtile = currentTile.GetComponent<tile>();
        newtile.setStatus("player", currentFloorNumber, newtile.tileNumber);
    }


}
