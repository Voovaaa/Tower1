using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class battleLogic : MonoBehaviour
{
    public GameObject floor;
    public GameObject scripts;
    public GameObject playerInstance;
    public GameObject enemyInstance;
    public string turn;
    public void Awake()
    {
        playerInstance.GetComponent<playerInBattle>().Awake();
        enemyInstance.GetComponent<enemy>().Awake();
        startBattle();
        turn = "player";
        InvokeRepeating("attack", 1f, 1f);
        Debug.Log("battle awake");
    }

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
        floor.SetActive(false);
        playerInstance.SetActive(true);
        enemyInstance.SetActive(true);
    }
    public void endBattle()
    {
        floor.SetActive(true);
        playerInstance.SetActive(false);
        enemyInstance.SetActive(false);
        scripts.GetComponent<game>().battleEnd();
    }
}
