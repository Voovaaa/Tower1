using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class saveLogicq
{
    
    
}
//public static class savesTool
//{
//    public static void reWriteSave(string saveName, string dataKey, string dataValue)
//    {
//        Dictionary<string, string> saveData = getSaveData(saveName);
//        saveData[dataKey] = dataValue;
//        setSaveData(saveName, saveData);
//    }
//    public static Dictionary<string, string> getSaveData(string saveName)
//    {
//        Dictionary<string, string> saveData = new Dictionary<string, string>();
//        string saveText = getSaveText(saveName);
//        string[] keysNvalues = saveText.Split(";");
//        string[] keyNvalue;
//        for (int i = 0; i < keysNvalues.Length; i++)
//        {
//            keyNvalue = keysNvalues[i].Split(" ");
//            saveData.Add(keyNvalue[0], keyNvalue[1]);
//        }

//        return saveData;
//    }
//    public static void setSaveData(string saveName, Dictionary<string, string> saveData)
//    {
//        string saveText = "";
//        foreach (KeyValuePair<string, string> dataPair in saveData)
//        {
//            saveText += $"{dataPair.Key} {dataPair.Value};";
//        }
//        saveText = saveText.Remove(saveText.Length - 1);
//        changeFileTextTo(getSavePath(saveName), saveText);
//        //saveText = "";
//    }
//    public static void createFile(string path, string fileName)
//    {
//        StreamWriter file = File.CreateText($"{path}/{fileName}.txt");
//        file.Close();
//    }
//    public static void clearFile(string path)
//    {
//        StreamWriter file = new StreamWriter(path, false);
//        file.Write("");
//        file.Close();
//    }
//    public static string getSavePath(string saveName)
//    {
//        return $"{Application.dataPath}/{saveName}.txt";
//    }
//    public static string getSaveText(string saveName)
//    {
//        StreamReader saveFile = new StreamReader(getSavePath(saveName));
//        string text = saveFile.ReadToEnd();
//        saveFile.Close();
//        return text;
//    }
//    public static void changeFileTextTo(string path, string text)
//    {
//        StreamWriter file = new StreamWriter(path, false);
//        file.Write(text);
//        file.Close();
//    }
//}
