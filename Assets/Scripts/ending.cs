using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ending : MonoBehaviour
{
    public GameObject endingsCanvas;
    public GameObject floor10;
    public GameObject badEnding;
    public GameObject neutralEnding;
    public GameObject goodEnding;
    public GameObject toFloor10Button;
    public static GameObject canvasEndings;
    public void nextButton()
    {
        floor10.SetActive(false);
        switch (chooseEnding())
        {
            case "bad":
                badEnding.SetActive(true);
                break;
            case "neutral":
                neutralEnding.SetActive(true);
                break;
            case "good":
                goodEnding.SetActive(true);
                break;
        }
    }
    private string chooseEnding()
    {
        if (floor1Logic.villagersAmount <= 0)
        {
            return "bad";
        }
        if (floor1Logic.villagersAmount < 22)
        {
            return "neutral";
        }
        return "good";
    }
    public void toEndingButton()
    {
        
    }
}
