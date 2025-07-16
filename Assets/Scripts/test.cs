using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class test : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void testTextUpdate(TMP_Text testText)
    {

        //string lvl = saveLogic.getProfileSaveValue("lvl");
        //string xp = saveLogic.getProfileSaveValue("xp");
        //testText.text = $"lvl{lvl}, xp{xp}";
    }
    public void testQuitButton()
    {
        Application.Quit();
    }
    public void testGetXpButton()
    {
        player.gotXp(0.3f);
    }
    public void testResetButton()
    {
        saveLogic.createProfile(PlayerPrefs.GetString("currentProfileName"));
    }
}
