using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class floor1Logic : MonoBehaviour
{
    public TMP_Text villagers;
    public TMP_Text foodLeft;
    public TMP_Text time;
    public TMP_Text playerFood;

    public static int foodAmount;
    public static float timeAmount;
    public static int villagersAmount;

    public static float defaultTimeAmount;
    private void Update()
    {
        changeTexts();
    }

    public void initialize()
    {
        defaultTimeAmount = float.Parse(saveLogic.defaultProfileSaveData["timeLeft"]);
        foodAmount = int.Parse(player.profileData["foodLeft"]);
        timeAmount = float.Parse(player.profileData["timeLeft"]);
        villagersAmount = int.Parse(player.profileData["villagersLeft"]);
    }




    public void giveFood() // button
    {
        foodAmount += int.Parse(player.profileData["foodCollected"]);
        player.setProfileValue("foodCollected", "0");
    }
    public void changeTexts()
    {
        villagers.text = $"villagers left: {villagersAmount}";
        foodLeft.text = $"foodLeft left: {foodAmount}";
        time.text = $"time left before feeding villagers: {timeAmount}";
        playerFood.text = $"your food: {player.profileData["foodCollected"]}";
    }

    public void backToFloor()
    {
        game.scripts.GetComponent<game>().backgroundFloor.SetActive(true);
        game.scripts.GetComponent<game>().floorsUi.SetActive(true);
        game.scripts.GetComponent<game>().floorsCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
