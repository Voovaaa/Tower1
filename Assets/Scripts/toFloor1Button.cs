using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toFloor1Button : MonoBehaviour
{
    public void onClick()
    {
        game.scripts.GetComponent<game>().backgroundFloor.SetActive(false);
        game.scripts.GetComponent<game>().floorsCanvas.SetActive(false);
        game.scripts.GetComponent<game>().floor1GM.SetActive(true);
        game.scripts.GetComponent<game>().floorsUi.SetActive(false);
    }
}
