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
    public Sprite unarmedGoblinSpriteIdle;
    public Sprite unarmedGoblinSpriteAttack;
    public Sprite bearIdle;
    public Sprite bearAttack;
    public Sprite armedGoblinIdle;
    public Sprite armedGoblinAttack;
    public Sprite banditIdle;
    public Sprite banditAttack;
    public Sprite veteranIdle;
    public Sprite veteranAttack;
    public Sprite androidIdle;
    public Sprite androidAttack;
    public Sprite helicopterIdle;
    public Sprite helicopterAttack;

    public bool alive;
    public Sprite currentSpriteIdle;
    public Sprite currentSpriteAttack;

    
    private GameObject battle;
    private float hpMax;
    private float hpCurrent;
    private int armor;
    private float damage;
    private float xp;

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
            case "wolf": // 3
                hpMax = 8;
                armor = 0;
                damage = 4;
                xp = 0.4f;
                currentSpriteIdle = wolfSpriteIdle;
                currentSpriteAttack = wolfSpriteAttack;
                break;
            case "circle":
                hpMax = 2;
                armor = 0;
                damage = 0;
                xp = 0.9f;
                currentSpriteIdle = circleSprite;
                currentSpriteAttack = circleSprite;
                break;
            case "unarmedGoblin": //2
                hpMax = 4;
                armor = 0;
                damage = 2; 
                xp = 0.3f;
                currentSpriteIdle = unarmedGoblinSpriteIdle;
                currentSpriteAttack = unarmedGoblinSpriteAttack;
                break;
            case "bear": // 4
                hpMax = 15;
                armor = 2;
                damage = 7;
                xp = 0.5f;
                currentSpriteAttack = bearAttack;
                currentSpriteIdle = bearIdle;
                break;
            case "armedGoblin": //5
                hpMax = 12;
                armor = 5;
                damage = 8;
                xp = 0.6f;
                currentSpriteAttack = armedGoblinAttack;
                currentSpriteIdle = armedGoblinIdle;
                break;
            case "bandit": // 6
                hpMax = 13;
                armor = 5;
                damage = 13;
                xp = 0.7f;
                currentSpriteAttack = banditAttack;
                currentSpriteIdle = banditIdle;
                break;
            case "veteran": // 7
                hpMax = 15;
                armor = 7;
                damage = 15;
                xp = 0.8f;
                currentSpriteAttack = veteranAttack;
                currentSpriteIdle = veteranIdle;
                break;
            case "android": // 8
                hpMax = 20;
                armor = 9;
                damage = 18;
                xp = 0.9f;
                currentSpriteAttack = androidAttack;
                currentSpriteIdle = androidIdle;
                break;
            case "helicopter": // 9
                hpMax = 30;
                armor = 13;
                damage = 20;
                xp = 1f;
                currentSpriteAttack = helicopterAttack;
                currentSpriteIdle = helicopterIdle;
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
        player.gotXp(xp);
        player.currentFloor.enemyDied(game.enemyToSpawnName);
    }
}
