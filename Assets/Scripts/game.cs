using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class game : MonoBehaviour
{
    //public TMP_Text text;
    //public TMP_InputField inputField; test
    private void Awake()
    {
        saveLogic.InitializeDefaultProfileSaveData();
        saveLogic.getProfileSaveData(saveLogic.currentProfileSaveName);
        Debug.Log(saveLogic.currentProfileSaveName);
        Debug.Log(PlayerPrefs.GetString($"{saveLogic.currentProfileSaveName} text"));

        //saveLogic.currentProfileSaveName = "huy"; toje test
        //saveLogic.currentProfileSaveData = saveLogic.getProfileSaveData(saveLogic.currentProfileSaveName);
        //text.text = saveLogic.currentProfileSaveData["text"];
    }
//    public void inputChanged(string inputText) // testovaya xuita
//    {
//        text.text = inputText;
//        saveLogic.currentProfileSaveData["text"] = inputText;
//        saveLogic.setProfileSave(saveLogic.currentProfileSaveName, saveLogic.currentProfileSaveData);
//    }
}
