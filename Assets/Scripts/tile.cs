using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{
    public string status; // unknown, wasHere, player
    public string currentMarkImageName;

    private void Start()
    {
        setStatus(status);
    }

    public void setStatus(string newStatus)
    {
        status = newStatus;
        setMark();
    }
    public void setMark() //questionMark, Xmark, playerMark наху€ этот метод
    {
        setMarkImage();
        //switch (status)
        //{
        //    case "unknown":
        //        setMarkImage();
        //        break;
        //    case "wasHere":
        //        setMarkImage();
        //        break;
        //    case "player":
        //        setMarkImage();
        //        break;
        //}
    }
    
    public void doTileMethod() // onclick 
    {
        switch (status)
        {
            case "unknown":
                unknownTileButton();
                break;
            case "wasHere":
                wasHereTileButton();
                break;
            case "player":
                playerTileButton();
                break;
            default:
                unknownTileButton();
                break;
        }
        player.moveToTile(transform.gameObject);
    }
    public void unknownTileButton()
    {
        int enemiesAmount = int.Parse(saveLogic.getFloorSaveValue(player.currentFloorNumber, "enemiesAmount"));
        // Debug.Log($"{player.currentFloor.unknownTilesAmount} {enemiesAmount}");
        if (enemiesAmount == player.currentFloor.unknownTilesAmount)
        {
            enemybattleorsomethinglikethat();
        }
        else if (enemiesAmount != 0 && Random.value >= 0.8f)
        {
            enemybattleorsomethinglikethat();
        }
        //Debug.Log($"{player.currentFloor.unknownTilesAmount} {enemiesAmount}");
        player.currentFloor.unknownTilesAmount -= 1;
        Debug.Log("unknownMethod");
    }
    public void wasHereTileButton()
    {
        Debug.Log("wasHereMethod");
    }
    public void playerTileButton()
    {
        Debug.Log("playerMethod");
    }

    public void enemybattleorsomethinglikethat()
    {
        Debug.Log("Butle");
        player.currentFloor.enemiesAmount -= 1;
        saveLogic.setFloorSaveValue(player.currentFloorNumber, "enemiesAmount", (player.currentFloor.enemiesAmount).ToString());
    }

    public void setMarkImage()
    {
        GameObject markImage;
        if (currentMarkImageName != null) { transform.Find(currentMarkImageName).gameObject.SetActive(false); }
        switch (status)
        {
            case "unknown":
                currentMarkImageName = "question mark";
                markImage = transform.Find("question mark").gameObject;
                break;
            case "wasHere":
                currentMarkImageName = "X mark";
                markImage = transform.Find("X mark").gameObject;
                break;
            case "player":
                currentMarkImageName = "player mark";
                markImage = transform.Find("player mark").gameObject;
                break;
            default:
                currentMarkImageName = "question mark";
                markImage = transform.Find("question mark").gameObject;
                break;
        }
        markImage.SetActive(true);
    }
}
