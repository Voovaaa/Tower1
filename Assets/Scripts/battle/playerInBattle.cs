using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerInBattle : MonoBehaviour
{
    public GameObject battle;
    public float hpMax;
    public float hpCurrent;
    public int armor;
    public float damage;
    public bool alive;
    public TMP_Text hpText;


    public void startBattle()
    {
        hpMax = float.Parse(player.getProfileValue("hpMax"));
        hpCurrent = hpMax;
        armor = player.calculateArmorValue();
        damage = player.calculateDamageValue();
        alive = true;
    }

    private void Update()
    {
        hpText.text = $"{hpCurrent} / {hpMax}";
    }

    public void attack()
    {
        battle.GetComponent<battleLogic>().enemyInstance.GetComponent<enemy>().attacked(damage);
    }
    public void attacked(float damage)
    {
        float damageToHp = damage - armor;
        if (damageToHp > 0)
        {
            hpCurrent -= damageToHp;
        }
        if (hpCurrent <= 0)
        {
            die();
        }
    }
    public void die()
    {
        alive = false;
        if (int.Parse(player.profileData["foodCollected"]) <= 10)
        {
            player.profileData["foodCollected"] = "0";
        }
        else
        {
            player.profileData["foodCollected"] = (int.Parse(player.profileData["foodCollected"]) - 10).ToString();
        }
        saveLogic.setProfileSaveValue("foodCollected", player.profileData["foodCollected"]);
        game.scripts.GetComponent<game>().notify("You lost your food");
    }
}
