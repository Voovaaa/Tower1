using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{
    public GameObject currentFloor;
    public GameObject battle;

    public int defaultFloorNumber;
    public GameObject tileToSpawnOnFloor2;

    public static string enemyToSpawnName;
    public static floor floor2;

    private loot[] lootsToDefaultSpawnFloor2; // переделать в список
    private loot lootuha;
    private void Awake()
    {
        enemyToSpawnName = "wolf"; // defaultName
        PlayerPrefs.DeleteAll(); // testMode

        Dictionary<string, string> enemiesNamountFloor2 = new Dictionary<string, string>();
        enemiesNamountFloor2["wolf"] = "5";
        enemiesNamountFloor2["circle"] = "1";
        loot lootuha = new loot("hueta", 0, 1);
        lootsToDefaultSpawnFloor2 = new loot[1];
        lootsToDefaultSpawnFloor2[0] = lootuha;
        floor2 = new floor(2, tileToSpawnOnFloor2, 92 - 1, lootsToDefaultSpawnFloor2, enemiesNamountFloor2);


        player.currentFloorNumber = defaultFloorNumber;
        player.currentFloor = floor2; // change it later
    }

    public class floor
    {
        public GameObject tileToSpawnOn;
        public int floorNumber;
        public int enemiesAmount;
        public bool wasHere;
        public int unknownTilesAmount;
        public loot[] availableLoot; // all loot not from enemies, from unknown tiles
        public Dictionary<string, string> enemiesNamounts;

        public floor(int floorNumber, GameObject tileToSpawn, int unknownTilesAmount, loot[] availableLoot, Dictionary<string, string> enemiesNamounts) // floor number and default floor data
        {
            this.floorNumber = floorNumber;
            this.unknownTilesAmount = unknownTilesAmount;
            string wasHereString = saveLogic.getFloorSaveValue(floorNumber, "wasHere");
            if (wasHereString != "" && bool.Parse(wasHereString)) //load floor save
            {
            }
            else //load default data if wasnt on floor
            {
                wasHere = false;
                saveLogic.setFloorSaveValue(floorNumber, "wasHere", wasHere.ToString());
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
                    saveLogic.setFloorSaveValue(floorNumber, $"LOOT{lootInstance.name}", "true");
                }
            }
            tileToSpawnOn = tileToSpawn;
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

    public class loot
    {
        public string name;
        public int damageValue;
        public int armorValue;

        public loot(string Name, int DamageValue = 0, int ArmorValue = 0)
        {
            name = Name;
            damageValue = DamageValue;
            armorValue = ArmorValue;
        }
    }


    public void battleStart()
    {
        battle.SetActive(true);
        battle.GetComponent<battleLogic>().startBattle();
    }
    public void battleEnd()
    {
        battle.SetActive(false);
    }


    public GameObject getTileToSpawn()
    {
        switch (player.currentFloorNumber)
        {
            case 2:
                return tileToSpawnOnFloor2;
            default:
                return tileToSpawnOnFloor2;
        }
    }
}
