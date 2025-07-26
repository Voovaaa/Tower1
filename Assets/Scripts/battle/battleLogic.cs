using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class battleLogic : MonoBehaviour
{
    public GameObject scripts;


    private GameObject floor;
    public GameObject playerInstance;
    public GameObject enemyInstance;
    private float repeatAttackTime;
    private string turn;
    private GameObject creatureToAnimate;
    public void attack()
    {
        if (playerInstance.GetComponent<playerInBattle>().alive && enemyInstance.GetComponent<enemy>().alive)
        {
            if (turn == "player")
            {
                playerInstance.GetComponent<playerInBattle>().attack();
                turn = "enemy";
                creatureToAnimate = playerInstance;
                doAttackAnimation();
            }
            else
            {
                enemyInstance.GetComponent<enemy>().attack();
                turn = "player";
                creatureToAnimate = enemyInstance;
                doAttackAnimation();
            }
        }
        else { endBattle(); }
    }    


    public void doAttackAnimation()
    {
        GameObject idleSprite = creatureToAnimate.transform.Find("idle").gameObject;
        GameObject attackSprite = creatureToAnimate.transform.Find("attack").gameObject;
        if (idleSprite.activeInHierarchy)
        {
            idleSprite.SetActive(false);
            attackSprite.SetActive(true);
            Invoke("doAttackAnimation", repeatAttackTime/3); // i like magic numbers, so what?
        }
        else
        {
            idleSprite.SetActive(true);
            attackSprite.SetActive(false);
        }
    }
    public void startBattle()
    {
        repeatAttackTime = 1f;

        floor = scripts.GetComponent<game>().currentFloor;
        enemyInstance = transform.Find("Enemy").gameObject;
        playerInstance = transform.Find("Player").gameObject;
        floor.SetActive(false);
        playerInstance.SetActive(true);
        enemyInstance.SetActive(true);
        //create enemy instance, take enemy name from floor save

        playerInstance.GetComponent<playerInBattle>().startBattle();
        enemyInstance.GetComponent<enemy>().startBattle();
        turn = "player";
        InvokeRepeating("attack", repeatAttackTime, repeatAttackTime);
    }
    public void endBattle()
    {
        CancelInvoke("attack");
        floor.SetActive(true);
        playerInstance.SetActive(false);
        enemyInstance.SetActive(false);
        scripts.GetComponent<game>().battleEnd();
    }
}
