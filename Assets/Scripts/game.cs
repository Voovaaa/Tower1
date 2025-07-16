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
    private void Awake()
    {
        player.currentFloorNumber = defaultFloorNumber;
        player.currentFloor = defaultFloor; // !
    }
    private void Start()
    {
    }
    private void Update()
    {
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
