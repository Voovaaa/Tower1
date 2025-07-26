using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chooseSave : MonoBehaviour
{
    public int saveNumber;
    public void onClick()
    {
        PlayerPrefs.SetString("currentProfileName", "profile " + saveNumber.ToString());
        SceneManager.LoadScene("Game");
        Debug.Log($"loaded save {saveNumber}");
    }
}
