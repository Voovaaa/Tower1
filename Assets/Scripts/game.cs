using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class game : MonoBehaviour
{
    public static GameObject scripts;

    public GameObject backgroundFloor;
    public GameObject backgroundBattle;

    public GameObject floorsCanvas;
    public GameObject floor1;
    public GameObject currentFloor;
    public GameObject battle;

    public int defaultFloorNumber;
    public GameObject tileToSpawnOnFloor2;

    public GameObject floorsUi;
    public GameObject lvlupMenu;

    public static string enemyToSpawnName;
    public static floor floor2;

    private List<loot> lootToDefaultSpawnFloor2;
    private loot lootuha;
    private void Awake()
    {
        scripts = transform.gameObject;
        //PlayerPrefs.DeleteAll(); // testMode

        saveLogic.initializeAllLoot();
        player.initialize();
        lvlUpButtons.Scripts = transform.gameObject;
        floor1.GetComponent<floor1Logic>().initialize();

        Dictionary<string, string> enemiesNamountFloor2 = new Dictionary<string, string>();
        enemiesNamountFloor2["wolf"] = "5";
        enemiesNamountFloor2["circle"] = "1";
        lootToDefaultSpawnFloor2 = saveLogic.floorNloot[2];
        int defaultAvailableFoodOnFloor2 = 30;
        floor2 = new floor(2, tileToSpawnOnFloor2, 92 - 1, lootToDefaultSpawnFloor2, enemiesNamountFloor2, defaultAvailableFoodOnFloor2);


        player.currentFloorNumber = defaultFloorNumber;
        player.currentFloor = floor2; // change it later


        //player.setProfileValue("foodCollected", "1");


    }

    private void Update() // убрать всё из апдейта нахуй, да хотя похуй лан
    {
        floorsUi.transform.Find("armor text").GetComponent<TMP_Text>().text = $"{player.armor.name.FirstCharacterToUpper()}: {player.calculateArmorValue()} armor.";
        floorsUi.transform.Find("weapon text").GetComponent<TMP_Text>().text = $"{player.weapon.name.FirstCharacterToUpper()}: {player.calculateDamageValue()} damage.";
        floorsUi.transform.Find("food collected").GetComponent<TMP_Text>().text = $"food collected: {player.profileData["foodCollected"]}";

        lvlupMenu.transform.Find("current lvl text").GetComponent<TMP_Text>().text = $"lvl: {player.profileData["lvl"]}";
        lvlupMenu.transform.Find("max hp").GetComponent<TMP_Text>().text = $"max hp: {player.profileData["hpMax"]}";
        lvlupMenu.transform.Find("armor").GetComponent<TMP_Text>().text = $"base armor: {player.profileData["armorValue"]}";
        lvlupMenu.transform.Find("damage").GetComponent<TMP_Text>().text = $"base damage: {player.profileData["damage"]}";
        lvlupMenu.transform.Find("skillpoints").GetComponent<TMP_Text>().text = $"skillpoints: {player.profileData["skillPoints"]}";


        floor1Logic.timeAmount -= Time.deltaTime;
        player.setProfileValue("timeLeft", floor1Logic.timeAmount.ToString());
        if (floor1Logic.timeAmount < 0 )
        {
            feedVillagers();
        }
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
        public int availableFood;

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
        floorsUi.transform.Find("notification text").GetComponent<TMP_Text>().text = notificationText;
        setNotificationActive();
        Invoke("setNotificationActive", 2f); // i love magic numbers
    }
    private void setNotificationActive()
    {
        GameObject txt = floorsUi.transform.Find("notification text").gameObject;
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
        floorsUi.SetActive(false);
        backgroundFloor.SetActive(false);
        backgroundBattle.SetActive(true);
        battle.SetActive(true);
        battle.GetComponent<battleLogic>().startBattle();
    }
    public void battleEnd()
    {
        floorsUi.SetActive(true);
        backgroundFloor.SetActive(true);
        backgroundBattle.SetActive(false);
        battle.SetActive(false);
    }

    public void feedVillagers()
    {
        floor1Logic.timeAmount = floor1Logic.defaultTimeAmount;
        floor1Logic.foodAmount -= floor1Logic.villagersAmount;
        if (floor1Logic.foodAmount < 0)
        {
            floor1Logic.villagersAmount -= floor1Logic.foodAmount * -1;
            floor1Logic.foodAmount = 0;
        }

        if (floor1Logic.villagersAmount <= 0)
        {
            floor1.transform.Find("npcs").gameObject.SetActive(false);
        }
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
