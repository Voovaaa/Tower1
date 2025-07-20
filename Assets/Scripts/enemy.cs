using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject battle;
    public string enemyName;
    public GameObject playerInstance;
    public float hpMax;
    public float hpCurrent;
    public int armor;
    public float damage;
    public bool alive;
    public TMP_Text hpText;

    public void Awake()
    {
        hpMax = 3;
        armor = 0;
        damage = 1;

        alive = true;
        // load default data from enemy save by name notyetamigo
        hpCurrent = hpMax;
        Debug.Log("enemy awake");

    }
    private void Update()
    {
        hpText.text = $"{hpCurrent} / {hpMax}";
    }

    public void attack()
    {
        Debug.Log($"{hpCurrent}enemy attacks");
        battle.GetComponent<battleLogic>().playerInstance.GetComponent<playerInBattle>().attacked(damage);
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
        player.currentFloor.enemiesAmount -= 1;
        saveLogic.setFloorSaveValue(player.currentFloorNumber, "enemiesAmount", player.currentFloor.enemiesAmount.ToString());
    }
}
