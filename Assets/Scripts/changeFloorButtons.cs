using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class changeFloorButtons : MonoBehaviour
{
    public static GameObject menu;
    private game scripts;

    private void Awake()
    {
        scripts = game.scripts.GetComponent<game>();
    }
    public void toFloor1()
    {
        scripts.backgroundFloor.SetActive(false);
        scripts.floorsCanvas.SetActive(false);
        scripts.floor1GM.SetActive(true);
        transform.parent.gameObject.SetActive(false);
        scripts.floorsUi.SetActive(false);

        
    }
    public void toFloorN(int floorNumber)
    {
        transform.parent.gameObject.SetActive(false);
        scripts.currentFloor.SetActive(false);
        scripts.currentFloor = scripts.getFloorGMByNumber(floorNumber);
        scripts.currentFloor.SetActive(true);
        scripts.floorsCanvas.SetActive(true);

        player.currentTile = scripts.getTileToSpawn(floorNumber);
        //player.moveToTile(player.currentTile);
        player.currentFloorNumber = floorNumber;
        player.currentFloor = scripts.getFloorByNumber(floorNumber);
        tile.initializeTiles(scripts.getFloorGMByNumber(floorNumber), floorNumber);
    }
    
    public void openMenu()
    {
        menu.SetActive(true);
        game.scripts.GetComponent<game>().floorsCanvas.SetActive(false);
        for (int i = 2; i <= 3; i++) // ADDING FLOORS, CHANGE IT
        {
            if (saveLogic.getFloorSaveValue(i, "wasHere") == "false")
            {
                menu.transform.Find($"to floor {i}").gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                menu.transform.Find($"to floor {i}").gameObject.GetComponent<Button>().interactable = true;
            }
        }
    }

    

}
