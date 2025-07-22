using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public TMP_Text hpText;
    public Sprite wolfSpriteIdle;
    public Sprite wolfSpriteAttack;
    public Sprite circleSprite;

    public bool alive;



    private GameObject battle;
    private float hpMax;
    private float hpCurrent;
    private int armor;
    private float damage;
    private string enemySpriteIdleName;
    private string enemySpriteAttackName;

    public void startBattle()
    {
        battle = transform.parent.gameObject;
        initializeEnemy();

        alive = true;
        hpCurrent = hpMax;
    }
    public void initializeEnemy()
    {
        enemySpriteAttackName = "enemy attack";
        enemySpriteIdleName = "enemy idle";
        switch (game.enemyToSpawnName)
        {
            case "wolf":
                hpMax = 3;
                armor = 0;
                damage = 1;
                transform.Find(enemySpriteIdleName).GetComponent<SpriteRenderer>().sprite = wolfSpriteIdle;
                transform.Find(enemySpriteAttackName).GetComponent<SpriteRenderer>().sprite = wolfSpriteAttack;
                break;
            case "circle":
                hpMax = 2;
                armor = 0;
                damage = 0;
                transform.Find(enemySpriteIdleName).GetComponent<SpriteRenderer>().sprite = circleSprite;
                transform.Find(enemySpriteAttackName).GetComponent<SpriteRenderer>().sprite = circleSprite;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        hpText.text = $"{hpCurrent} / {hpMax}";
    }

    public void attack()
    {
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
        player.currentFloor.enemiesNamounts[game.enemyToSpawnName] = (int.Parse(player.currentFloor.enemiesNamounts[game.enemyToSpawnName]) - 1).ToString();
        saveLogic.setFloorSaveValue(player.currentFloorNumber, "enemiesAmount", player.currentFloor.enemiesAmount.ToString()); // убрать из сейва этого врага
    }
}
