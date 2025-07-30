using System.Collections.Generic;
using UnityEngine;

public class floor
{
    public GameObject tileToSpawnOn;
    public int floorNumber;
    public int enemiesAmount;
    public bool wasHere;
    public int unknownTilesAmount;
    public List<loot> availableLoot; // all loot not from enemies, from unknown tiles
    public Dictionary<string, string> enemiesNamounts;
    public int availableFood;
    public bool ladderFound;

    public floor(int floorNumber, GameObject tileToSpawn, int unknownTilesAmount, List<loot> availableLoot, Dictionary<string, string> enemiesNamounts, int availableFood) // floor number and default floor data
    {
        this.floorNumber = floorNumber;
        this.unknownTilesAmount = unknownTilesAmount;
        string wasHereString = saveLogic.getFloorSaveValue(floorNumber, "wasHere");
        if (wasHereString != "" && bool.Parse(wasHereString)) //load floor save
        {
            enemiesAmount = int.Parse(saveLogic.getFloorSaveValue(floorNumber, "enemiesAmount"));

            this.unknownTilesAmount = int.Parse(saveLogic.getFloorSaveValue(floorNumber, "unknownTilesAmount"));

            this.availableFood = int.Parse(saveLogic.getFloorSaveValue(floorNumber, "availableFood"));

            foreach (loot lootInstance in availableLoot)
            {
                if (saveLogic.getFloorSaveValue(floorNumber, $"loot {lootInstance.name}") == "true")
                {
                    this.availableLoot.Add(lootInstance);
                }
            }

            foreach (KeyValuePair<string, string> kvp in enemiesNamounts)
            {
                if (int.Parse(saveLogic.getFloorSaveValue(floorNumber, $"enemy {kvp.Key}")) > 0)
                {
                    this.enemiesNamounts[kvp.Key] = saveLogic.getFloorSaveValue(floorNumber, $"enemy {kvp.Key}");
                }
            }
        }
        else //load default data if wasnt on floor
        {
            wasHere = false;
            saveLogic.setFloorSaveValue(floorNumber, "wasHere", wasHere.ToString().ToLower());

            ladderFound = false;
            saveLogic.setFloorSaveValue(floorNumber, "ladderFound", "false");

            this.enemiesNamounts = enemiesNamounts;
            foreach (KeyValuePair<string, string> kvp in enemiesNamounts)
            {
                saveLogic.setFloorSaveValue(floorNumber, $"enemy {kvp.Key}", kvp.Value);
            }

            enemiesAmount = calculateEnemiesAmount();
            saveLogic.setFloorSaveValue(floorNumber, "enemiesAmount", enemiesAmount.ToString());

            this.availableLoot = availableLoot;
            foreach (loot lootInstance in availableLoot)
            {
                saveLogic.setFloorSaveValue(floorNumber, $"loot {lootInstance.name}", "true");
            }

            this.unknownTilesAmount = unknownTilesAmount;
            saveLogic.setFloorSaveValue(floorNumber, "unknownTilesAmount", unknownTilesAmount.ToString());

            this.availableFood = availableFood;
            saveLogic.setFloorSaveValue(floorNumber, "availableFood", availableFood.ToString());
        }
        tileToSpawnOn = tileToSpawn;
    }
    public void decrementUnknownTiles()
    {
        unknownTilesAmount -= 1;
        saveLogic.setFloorSaveValue(floorNumber, "unknownTilesAmount", unknownTilesAmount.ToString());
    }
    public void enemyDied(string enemyName)
    {
        enemiesNamounts[enemyName] = (int.Parse(enemiesNamounts[enemyName]) - 1).ToString();
        saveLogic.setFloorSaveValue(floorNumber, $"enemy {enemyName}", enemiesNamounts[enemyName]);
        enemiesAmount -= 1;
        saveLogic.setFloorSaveValue(floorNumber, "enemiesAmount", enemiesAmount.ToString());
    }
    public void lootTaken(loot lootInstance)
    {
        availableLoot.Remove(lootInstance);
        saveLogic.setFloorSaveValue(floorNumber, $"loot {lootInstance.name}", "false");
    }


    public int calculateEnemiesAmount()
    {
        int amount = 0;
        foreach (KeyValuePair<string, string> enemyNamount in enemiesNamounts)
        {
            amount += int.Parse(enemyNamount.Value);
        }
        return amount;
    }
}

