using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class game : MonoBehaviour
{
    public int defaultFloorNumber;
    public GameObject defaultFloor;
    public GameObject tileToSpawnOnFloor2;
    public static floor floor2;
    private void Awake()
    {
        player.currentFloorNumber = defaultFloorNumber;
        player.currentFloor = defaultFloor; // !
        floor2 = new floor(2, 5);
    }

    public class floor
    {
        public GameObject tileToSpawnOn;
        public int floorNumber;
        public int enemiesAmount;
        public bool wasHere;

        public floor(int fN, int eA) // floor number and default floor data
        {
            floorNumber = fN;
            if (bool.Parse(saveLogic.getFloorSaveValue(fN, "wasHere"))) //load floor save
            {
            }
            else //load default data if wasnt on floor
            {
                eA = int.Parse(saveLogic.getFloorSaveValue(floorNumber, "enemies amount"));
            }
            enemiesAmount = eA;
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
    //public static GameObject getFloor(int floorNumber)
    //{
    //    return 
    //}
}
