using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{
    public GameObject scripts;
    public string status; // unknown, wasHere, player
    public string currentMarkImageName;

    private void Start()
    {
        scripts = GameObject.Find("Scripts");
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
    }
    public void unknownTileButton()
    {
        Vector2 clickedTilePosition = transform.position;
        Vector2 playerTilePosition = player.currentTile.transform.position;
        float tilesDistance = Vector2.Distance(clickedTilePosition, playerTilePosition);
        Debug.Log(tilesDistance);
        if (tilesDistance > 1.1f)
        {
            return;
        }
        int enemiesAmount = int.Parse(saveLogic.getFloorSaveValue(player.currentFloorNumber, "enemiesAmount"));
        if (enemiesAmount == player.currentFloor.unknownTilesAmount)
        {
            battleStart();
        }
        else if (enemiesAmount != 0 && Random.value >= 0.8f)
        {
            battleStart();
        }
        player.currentFloor.unknownTilesAmount -= 1;
        Debug.Log("unknownMethod");

        player.moveToTile(transform.gameObject);
    }
    public void wasHereTileButton()
    {
        Debug.Log("wasHereMethod");

        player.moveToTile(transform.gameObject);
    }
    public void playerTileButton()
    {
        Debug.Log("playerMethod");
    }

    public void battleStart()
    {
        scripts.GetComponent<game>().battleStart();
        Debug.Log("Butle");
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
