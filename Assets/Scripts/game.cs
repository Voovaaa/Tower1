using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class game : MonoBehaviour
{
    public GameObject backgroundFloor;
    public GameObject backgroundBattle;

    public GameObject currentFloor;
    public GameObject battle;

    public int defaultFloorNumber;
    public GameObject tileToSpawnOnFloor2;

    public GameObject ui;
    public GameObject lvlupMenu;

    public static string enemyToSpawnName;
    public static floor floor2;

    private List<loot> lootToDefaultSpawnFloor2;
    private loot lootuha;
    private void Awake()
    {
        PlayerPrefs.DeleteAll(); // testMode

        saveLogic.initializeAllLoot();
        player.initialize(transform.gameObject);
        lvlUpButtons.Scripts = transform.gameObject;

        Dictionary<string, string> enemiesNamountFloor2 = new Dictionary<string, string>();
        enemiesNamountFloor2["wolf"] = "5";
        enemiesNamountFloor2["circle"] = "1";
        lootToDefaultSpawnFloor2 = saveLogic.floorNloot[2];
        floor2 = new floor(2, tileToSpawnOnFloor2, 92 - 1, lootToDefaultSpawnFloor2, enemiesNamountFloor2);


        player.currentFloorNumber = defaultFloorNumber;
        player.currentFloor = floor2; // change it later


    }

    private void Update() // убрать всё из апдейта нахуй, да хотя похуй лан
    {
        ui.transform.Find("armor text").GetComponent<TMP_Text>().text = $"{player.armor.name.FirstCharacterToUpper()}: {player.calculateArmorValue()} armor.";
        ui.transform.Find("weapon text").GetComponent<TMP_Text>().text = $"{player.weapon.name.FirstCharacterToUpper()}: {player.calculateDamageValue()} damage.";

        lvlupMenu.transform.Find("current lvl text").GetComponent<TMP_Text>().text = $"lvl: {player.profileData["lvl"]}";
        lvlupMenu.transform.Find("max hp").GetComponent<TMP_Text>().text = $"max hp: {player.profileData["hpMax"]}";
        lvlupMenu.transform.Find("armor").GetComponent<TMP_Text>().text = $"base armor: {player.profileData["armorValue"]}";
        lvlupMenu.transform.Find("damage").GetComponent<TMP_Text>().text = $"base damage: {player.profileData["damage"]}";
        lvlupMenu.transform.Find("skillpoints").GetComponent<TMP_Text>().text = $"skillpoints: {player.profileData["skillPoints"]}";
    }

    public class floor
    {
        public GameObject tileToSpawnOn;
        public int floorNumber;
        public int enemiesAmount;
        public bool wasHere;
        public int unknownTilesAmount;
        public List<loot> availableLoot; // all loot not from enemies, from unknown tiles
        public Dictionary<string, string> enemiesNamounts;

        public floor(int floorNumber, GameObject tileToSpawn, int unknownTilesAmount, List<loot> availableLoot, Dictionary<string, string> enemiesNamounts) // floor number and default floor data
        {
            this.floorNumber = floorNumber;
            this.unknownTilesAmount = unknownTilesAmount;
            string wasHereString = saveLogic.getFloorSaveValue(floorNumber, "wasHere");
            if (wasHereString != "" && bool.Parse(wasHereString)) //load floor save
            {
                enemiesAmount = int.Parse(saveLogic.getFloorSaveValue(floorNumber, "enemiesAmount"));

                unknownTilesAmount = int.Parse(saveLogic.getFloorSaveValue(floorNumber, "unknownTilesAmount"));

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
                    saveLogic.setFloorSaveValue(floorNumber, $"loot {lootInstance.name}", "true");
                }

                this.unknownTilesAmount = unknownTilesAmount;
                saveLogic.setFloorSaveValue(floorNumber, "unknownTilesAmount", unknownTilesAmount.ToString());
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

    public void notify(string notificationText)
    {
        ui.transform.Find("notification text").GetComponent<TMP_Text>().text = notificationText;
        setNotificationActive();
        Invoke("setNotificationActive", 2f); // i love magic numbers
    }
    private void setNotificationActive()
    {
        GameObject txt = ui.transform.Find("notification text").gameObject;
        if (txt.activeInHierarchy)
        {
            txt.SetActive(false);
        }
        else
        {
            txt.SetActive(true);
        }
    }
    public void battleStart()
    {
        ui.SetActive(false);
        backgroundFloor.SetActive(false);
        backgroundBattle.SetActive(true);
        battle.SetActive(true);
        battle.GetComponent<battleLogic>().startBattle();
    }
    public void battleEnd()
    {
        ui.SetActive(true);
        backgroundFloor.SetActive(true);
        backgroundBattle.SetActive(false);
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
