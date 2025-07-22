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
    private string turn;
    public void attack()
    {
        if (playerInstance.GetComponent<playerInBattle>().alive && enemyInstance.GetComponent<enemy>().alive)
        {
            if (turn == "player")
            {
                playerInstance.GetComponent<playerInBattle>().attack();
                turn = "enemy";
            }
            else
            {
                enemyInstance.GetComponent<enemy>().attack();
                turn = "player";
            }
            Debug.Log($"now {turn} turn");
        }
        else { endBattle(); }
    }    

    public void startBattle()
    {
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
        InvokeRepeating("attack", 1f, 1f);
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
