using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class game : MonoBehaviour
{
    public int defaultFloorNumber;
    public GameObject tileToSpawnOnFloor2;
    public static floor floor2;

    public loot[] lootsToDefaultSpawnFloor2;
    public loot lootuha;
    private void Awake()
    {
        loot lootuha = new loot("hueta", 0,1);
        lootsToDefaultSpawnFloor2 = new loot[9999];
        lootsToDefaultSpawnFloor2[0] = lootuha;
        player.currentFloorNumber = defaultFloorNumber;
        floor2 = new floor(2, 5, tileToSpawnOnFloor2, 92-1, lootsToDefaultSpawnFloor2);
        player.currentFloor = floor2; // change it later
    }

    public class floor
    {
        public GameObject tileToSpawnOn;
        public int floorNumber;
        public int enemiesAmount;
        public bool wasHere;
        public int unknownTilesAmount;
        public loot[] availableLoot;

        public floor(int fN, int eA, GameObject tileToSpawn, int unknownTiles, loot[] loots) // floor number and default floor data
        {
            floorNumber = fN;
            unknownTilesAmount = unknownTiles;
            string wasHereString = saveLogic.getFloorSaveValue(floorNumber, "wasHere");
            if (wasHereString != "" && bool.Parse(wasHereString)) //load floor save
            {
            }
            else //load default data if wasnt on floor
            {
                wasHere = false;
                saveLogic.setFloorSaveValue(floorNumber, "wasHere", wasHere.ToString());
                enemiesAmount = eA;
                saveLogic.setFloorSaveValue(floorNumber, "enemiesAmount", enemiesAmount.ToString());
                availableLoot = loots;
                foreach (loot lootInstance in availableLoot)
                {
                    saveLogic.setFloorSaveValue(floorNumber, $"LOOT{lootInstance.name}", "true");
                }
            }
            tileToSpawnOn = tileToSpawn;
        }
    }

    public class loot
    {
        public string name;
        public int damageValue;
        public int armorValue;

        public loot(string Name, int DamageValue=0, int ArmorValue=0)
        {
            name = Name;
            damageValue = DamageValue;
            armorValue = ArmorValue;
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
