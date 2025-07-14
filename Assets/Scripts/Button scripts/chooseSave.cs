using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chooseSave : MonoBehaviour
{
    public int saveNumber;
    public void onClick()
    {
        // load save file
        SceneManager.LoadScene("Game");
        Debug.Log($"loaded save {saveNumber}");
    }
}
