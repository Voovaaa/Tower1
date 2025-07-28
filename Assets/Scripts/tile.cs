using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{
    public GameObject scripts;
    public string status; // unknown, wasHere, player
    public string currentMarkImageName;
    public float chanceToLoot = 0.1f;
    public float chanceToFood = 0.2f;
    public int tileNumber;
    private void Start()
    {
        scripts = GameObject.Find("Scripts");
        //setStatus(status, -1);
    }


    public string getStatus(int floorNumber, int tileNumb = -1)
    {
        if (tileNumb == -1) { tileNumb = tileNumber; }
        if (saveLogic.getFloorSaveValue(floorNumber, $"tile {tileNumb}") == "")
        {
            setStatus("unknown", floorNumber, tileNumb);
        }
        else
        {
            setStatus(saveLogic.getFloorSaveValue(floorNumber, $"tile {tileNumb}"), floorNumber, tileNumb);
        }
        return saveLogic.getFloorSaveValue(floorNumber, $"tile {tileNumb}");
    }
    public void setStatus(string newStatus, int floorNumber, int tileNumber)
    {
        status = newStatus;
        this.tileNumber = tileNumber;
        saveLogic.setFloorSaveValue(floorNumber, $"tile {tileNumber}", status);
        Debug.Log($"{tileNumber}, {floorNumber}, {status}");
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
        int foodFound = 0;
        if (tilesDistance > 1.1f)
        {
            return;
        }
        int enemiesAmount = int.Parse(saveLogic.getFloorSaveValue(player.currentFloorNumber, "enemiesAmount"));
        if (enemiesAmount == player.currentFloor.unknownTilesAmount)
        {
            battleStart();
            foodFound += 1;
        }
        else if (enemiesAmount != 0 && Random.value >= 0.8f)
        {
            battleStart();
            foodFound += 1;
        }
        player.currentFloor.decrementUnknownTiles();

        if (isGetFood())
        {
            foodFound += 1;
        }
        getFood(foodFound);


        if (isGetLoot())
        {
            player.foundLoot(getRandomLoot());
        }



        player.moveToTile(transform.gameObject);
    }
    public void wasHereTileButton()
    {
        player.moveToTile(transform.gameObject);
    }
    public void playerTileButton()
    {
    }

    public bool isGetFood()
    {
        if (player.currentFloor.unknownTilesAmount <= player.currentFloor.availableFood)
        {
            return true;
        }
        if (Random.value <= chanceToFood)
        {
            return true;
        }
        return false;
    }
    public void getFood(int foodAmount)
    {
        if (foodAmount == 0) { return; }
        if (foodAmount > player.currentFloor.availableFood)
        {
            foodAmount = player.currentFloor.availableFood;
        }
        player.currentFloor.availableFood -= foodAmount;
        player.setProfileValue("foodCollected", (int.Parse(player.profileData["foodCollected"]) + foodAmount).ToString());
        saveLogic.setFloorSaveValue(player.currentFloorNumber, $"availableFood", player.currentFloor.availableFood.ToString());
    }

    public bool isGetLoot()
    {
        List<game.loot> availableLoot = player.currentFloor.availableLoot;
        int unknownTilesAmount = player.currentFloor.unknownTilesAmount;
        if (availableLoot.Count <= 0)
        {
            return false;
        }
        if (unknownTilesAmount == availableLoot.Count)
        {
            return true;
        }
        if (Random.value <= chanceToLoot)
        {
            return true;
        }
        return false;
    }
    public game.loot getRandomLoot()
    {
        game.loot lootToReturn = player.currentFloor.availableLoot[Random.Range(0, player.currentFloor.availableLoot.Count)];
        return lootToReturn;
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
                continue;
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
