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
    private void Awake()
    {
        player.currentFloorNumber = defaultFloorNumber;
        floor2 = new floor(2, 5, tileToSpawnOnFloor2, 92-1);
        player.currentFloor = floor2; // change it later
    }

    public class floor
    {
        public GameObject tileToSpawnOn;
        public int floorNumber;
        public int enemiesAmount;
        public bool wasHere;
        public int unknownTilesAmount;

        public floor(int fN, int eA, GameObject tileToSpawn, int unknownTiles) // floor number and default floor data
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
            }
            tileToSpawnOn = tileToSpawn;
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
