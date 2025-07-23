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

        player.moveToTile(transform.gameObject);
    }
    public void wasHereTileButton()
    {
        player.moveToTile(transform.gameObject);
    }
    public void playerTileButton()
    {
    }

    public void battleStart() // выбрать энеми из доступных на этаже
    {
        game.enemyToSpawnName = getRandomEnemyNameToSpawn(player.currentFloor.enemiesNamounts);
        if (game.enemyToSpawnName == "") { return; }
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

    public static string getRandomEnemyNameToSpawn(Dictionary<string, string> dict)
    {

        int valuesAmount = dict.Count;
        int randomNumber = Random.Range(0, valuesAmount);
        string keyName = "";

        int i = 0;
        foreach (KeyValuePair<string, string> kvp in dict)
        {
            if (kvp.Value == "0")
            {
                i -= 1;
            }
            if (i == randomNumber)
            {
                keyName = kvp.Key;
                break;
            }

            i++;
        }
        return keyName;
    }
}
