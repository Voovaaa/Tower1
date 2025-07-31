using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public partial class game : MonoBehaviour
{
    public static GameObject scripts;

    public GameObject backgroundFloor;
    public GameObject backgroundBattle;

    public GameObject floorsCanvas;
    public GameObject floor1GM;
    public GameObject currentFloor;
    public GameObject battle;

    public int defaultFloorNumber;
    public GameObject tileToSpawnOnFloor2;
    public GameObject tileToSpawnOnFloor3;
    public GameObject tileToSpawnOnFloor4;
    public GameObject tileToSpawnOnFloor5;
    public GameObject tileToSpawnOnFloor6;
    public GameObject tileToSpawnOnFloor7;
    public GameObject tileToSpawnOnFloor8;
    public GameObject tileToSpawnOnFloor9;

    public GameObject floorsUi;
    public GameObject lvlupMenu;

    public GameObject floor2GM;
    public GameObject floor3GM;
    public GameObject floor4GM;
    public GameObject floor5GM;
    public GameObject floor6GM;
    public GameObject floor7GM;
    public GameObject floor8GM;
    public GameObject floor9GM;

    public static string enemyToSpawnName;

    public static floor floor2;
    public static floor floor3;
    public static floor floor4;
    public static floor floor5;
    public static floor floor6;
    public static floor floor7;
    public static floor floor8;
    public static floor floor9;

    private List<loot> lootToDefaultSpawnFloor2;
    private Dictionary<string, string> enemiesNamountFloor2;
    private List<loot> lootToDefaultSpawnFloor3;
    private Dictionary<string, string> enemiesNamountFloor3;
    private List<loot> lootToDefaultSpawnFloor4;
    private Dictionary<string, string> enemiesNamountFloor4;
    private List<loot> lootToDefaultSpawnFloor5;
    private Dictionary<string , string> enemiesNamountFloor5;
    private List<loot> lootToDefaultSpawnFloor6;
    private Dictionary<string, string> enemiesNamountFloor6;
    private List<loot> lootToDefaultSpawnFloor7;
    private Dictionary<string, string> enemiesNamountFloor7;
    private List<loot> lootToDefaultSpawnFloor8;
    private Dictionary<string, string> enemiesNamountFloor8;
    private List<loot> lootToDefaultSpawnFloor9;
    private Dictionary<string, string> enemiesNamountFloor9;

    // new floor: game: awake, get floors, floorNGM, floorN; savelogic: floorNloot; enemy: initialize+sprites; changeFloorButtons

    private void Awake()
    {
        scripts = transform.gameObject;
        PlayerPrefs.DeleteAll(); // testMode

        saveLogic.initializeAllLoot();

        enemiesNamountFloor2 = new Dictionary<string, string>();
        enemiesNamountFloor2["unarmedGoblin"] = "5";
        lootToDefaultSpawnFloor2 = saveLogic.floorNloot[2];
        int defaultAvailableFoodOnFloor2 = 30;
        floor2 = new floor(2, tileToSpawnOnFloor2, 92 - 1, lootToDefaultSpawnFloor2, enemiesNamountFloor2, defaultAvailableFoodOnFloor2);
        tile.initializeTiles(floor2GM, 2);
        floor2.wasHere = true;
        saveLogic.setFloorSaveValue(2, "wasHere", "true");

        enemiesNamountFloor3 = new Dictionary<string, string>();
        enemiesNamountFloor3["wolf"] = "4";
        lootToDefaultSpawnFloor3 = saveLogic.floorNloot[3];
        int defaultAvailableFoodOnFloor3 = 20;
        floor3 = new floor(3, tileToSpawnOnFloor3, 68 - 1, lootToDefaultSpawnFloor3, enemiesNamountFloor3, defaultAvailableFoodOnFloor3);
        tile.initializeTiles(floor3GM, 3);

        enemiesNamountFloor4 = new Dictionary<string, string>();
        enemiesNamountFloor4["bear"] = "3";
        lootToDefaultSpawnFloor4 = saveLogic.floorNloot[4];
        int defaultAvailableFoodOnFloor4 = 20;
        floor4 = new floor(4, tileToSpawnOnFloor4, 64 - 1, lootToDefaultSpawnFloor4, enemiesNamountFloor4, defaultAvailableFoodOnFloor4);

        enemiesNamountFloor5 = new Dictionary<string, string>();
        enemiesNamountFloor5["armedGoblin"] = "4";
        lootToDefaultSpawnFloor5 = saveLogic.floorNloot[5];
        int defaultAvailableFoodOnFloor5 = 25;
        floor5 = new floor(5, tileToSpawnOnFloor5, 52 - 1, lootToDefaultSpawnFloor5, enemiesNamountFloor5, defaultAvailableFoodOnFloor5);

        enemiesNamountFloor6 = new Dictionary<string, string>();
        enemiesNamountFloor6["bandit"] = "4";
        lootToDefaultSpawnFloor6 = saveLogic.floorNloot[6];
        int defaultAvailableFoodOnFloor6 = 30;
        floor6 = new floor(6, tileToSpawnOnFloor6, 40 - 1, lootToDefaultSpawnFloor6, enemiesNamountFloor6, defaultAvailableFoodOnFloor6);

        enemiesNamountFloor7 = new Dictionary<string, string>();
        enemiesNamountFloor7["veteran"] = "3";
        lootToDefaultSpawnFloor7 = saveLogic.floorNloot[7];
        int defaultAvailableFoodOnFloor7 = 15;
        floor7 = new floor(7, tileToSpawnOnFloor7, 24 - 1, lootToDefaultSpawnFloor7, enemiesNamountFloor7, defaultAvailableFoodOnFloor7);

        enemiesNamountFloor8 = new Dictionary<string, string>();
        enemiesNamountFloor8["android"] = "3";
        lootToDefaultSpawnFloor8 = saveLogic.floorNloot[8];
        int defaultAvailableFoodOnFloor8 = 4;
        floor8 = new floor(8, tileToSpawnOnFloor8, 12 - 1, lootToDefaultSpawnFloor8, enemiesNamountFloor8, defaultAvailableFoodOnFloor8);

        enemiesNamountFloor9 = new Dictionary<string, string>();
        enemiesNamountFloor9["helicopter"] = "1";
        lootToDefaultSpawnFloor9 = saveLogic.floorNloot[9];
        int defaultAvailableFoodOnFloor9 = 3;
        floor9 = new floor(9, tileToSpawnOnFloor9, 4 - 1, lootToDefaultSpawnFloor9, enemiesNamountFloor9, defaultAvailableFoodOnFloor9);

        player.initialize();
        lvlUpButtons.Scripts = transform.gameObject;
        floor1GM.GetComponent<floor1Logic>().initialize();


        player.currentFloorNumber = defaultFloorNumber;
        player.currentFloor = floor2; // change it later


        changeFloorButtons.menu = floorsUi.transform.Find("change floor menu").gameObject;
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
        if (floor1Logic.timeAmount < 0)
        {
            feedVillagers();
        }


    }

    public void notify(string notificationText)
    {
        floorsUi.transform.Find("notification text").GetComponent<TMP_Text>().text = notificationText;
        setNotificationActive();
        Invoke("setNotificationActive", 2f); // i love magic numbers
    }

    void setNotificationActive()
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
            floor1GM.transform.Find("npcs").gameObject.SetActive(false);
        }
    }

    public GameObject getTileToSpawn(int floorNumber)
    {
        return getFloorByNumber(floorNumber).tileToSpawnOn;
    }
    public floor getFloorByNumber(int floorNumber)
    {
        switch (floorNumber)
        {
            case 2:
                return floor2;
            case 3:
                return floor3;
            case 4:
                return floor4;
            case 5:
                return floor5;
            case 6:
                return floor6;
            case 7:
                return floor7;
            case 8:
                return floor8;
            case 9:
                return floor9;
            default:
                return null;
        }
    }
    public GameObject getFloorGMByNumber(int floorNumber)
    {
        switch (floorNumber)
        {
            case 2:
                return floor2GM;
            case 3:
                return floor3GM;
            case 4:
                return floor4GM;
            case 5:
                return floor5GM;
            case 6:
                return floor6GM;
            case 7:
                return floor7GM;
            case 8:
                return floor8GM;
            case 9:
                return floor9GM;
            default:
                return null;
        }
    }
}
