using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlUpButtons : MonoBehaviour
{
    public static GameObject Scripts;
    public void increaseHp()
    {
        if (player.profileData["skillPoints"] == "0") { return; }
        player.setProfileValue("skillPoints", (int.Parse(player.profileData["skillPoints"]) - 1).ToString());
        player.setProfileValue("hpMax", (int.Parse(player.profileData["hpMax"]) + 1).ToString());
    }
    public void increaseArmor()
    {
        if (player.profileData["skillPoints"] == "0") { return; }
        player.setProfileValue("skillPoints", (int.Parse(player.profileData["skillPoints"]) - 1).ToString());
        player.setProfileValue("armorValue", (int.Parse(player.profileData["armorValue"]) + 1).ToString());
    }
    public void increaseDamage()
    {
        if (player.profileData["skillPoints"] == "0") { return; }
        player.setProfileValue("skillPoints", (int.Parse(player.profileData["skillPoints"]) - 1).ToString());
        player.setProfileValue("damage", (int.Parse(player.profileData["damage"]) + 1).ToString());
    }
    public void toLvlup()
    {
        Scripts.GetComponent<game>().currentFloor.SetActive(false);
        Scripts.GetComponent<game>().floorsUi.SetActive(false);
        Scripts.GetComponent<game>().lvlupMenu.SetActive(true);

    }
    public void backToFloor()
    {
        Scripts.GetComponent<game>().currentFloor.SetActive(true);
        Scripts.GetComponent<game>().floorsUi.SetActive(true);
        Scripts.GetComponent<game>().lvlupMenu.SetActive(false);
        
    }
}
