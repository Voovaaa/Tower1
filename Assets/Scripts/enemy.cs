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
    public Sprite currentSpriteIdle;
    public Sprite currentSpriteAttack;

    
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
        switch (game.enemyToSpawnName)
        {
            case "wolf":
                hpMax = 4;
                armor = 0;
                damage = 2;
                currentSpriteIdle = wolfSpriteIdle;
                currentSpriteAttack = wolfSpriteAttack;
                break;
            case "circle":
                hpMax = 2;
                armor = 0;
                damage = 0;
                currentSpriteIdle = circleSprite;
                currentSpriteAttack = circleSprite;
                break;
            default:
                break;
        }
        transform.Find("idle").GetComponent<SpriteRenderer>().sprite = currentSpriteIdle;
        transform.Find("attack").GetComponent<SpriteRenderer>().sprite = currentSpriteAttack;
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
        player.currentFloor.enemyDied(game.enemyToSpawnName);
    }
}
