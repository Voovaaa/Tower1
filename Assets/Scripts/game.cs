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

    public GameObject floorsUi;
    public GameObject lvlupMenu;

    public GameObject floor2GM;
    public GameObject floor3GM;

    public static string enemyToSpawnName;

    public static floor floor2;
    public static floor floor3;

    private List<loot> lootToDefaultSpawnFloor2;
    private Dictionary<string, string> enemiesNamountFloor2;
    private List<loot> lootToDefaultSpawnFloor3;
    private Dictionary<string, string> enemiesNamountFloor3;
    private loot lootuha;

    public GameObject TIIILE;
    private void Awake()
    {
        scripts = transform.gameObject;
        PlayerPrefs.DeleteAll(); // testMode

        saveLogic.initializeAllLoot();

        enemiesNamountFloor2 = new Dictionary<string, string>();
        enemiesNamountFloor2["unarmedGoblin"] = "5";
        enemiesNamountFloor2["circle"] = "1";
        lootToDefaultSpawnFloor2 = saveLogic.floorNloot[2];
        int defaultAvailableFoodOnFloor2 = 30;
        floor2 = new floor(2, tileToSpawnOnFloor2, 92 - 1, lootToDefaultSpawnFloor2, enemiesNamountFloor2, defaultAvailableFoodOnFloor2);
        tile.initializeTiles(floor2GM, 2);
        floor2.wasHere = true;
        saveLogic.setFloorSaveValue(2, "wasHere", "true");

        enemiesNamountFloor3 = new Dictionary<string, string>();
        enemiesNamountFloor3["wolf"] = "4";
        enemiesNamountFloor3["circle"] = "1";
        lootToDefaultSpawnFloor3 = saveLogic.floorNloot[3];
        int defaultAvailableFoodOnFloor3 = 20;
        floor3 = new floor(3, tileToSpawnOnFloor3, 68, lootToDefaultSpawnFloor3, enemiesNamountFloor3, defaultAvailableFoodOnFloor3);
        tile.initializeTiles(floor3GM, 3);



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
            default:
                return null;
        }
    }
}
