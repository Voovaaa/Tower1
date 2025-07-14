using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMenu : MonoBehaviour
{
    public GameObject currentMenu;
    public GameObject nextMenu;
    public void onClick()
    {
        currentMenu.SetActive(false);
        nextMenu.SetActive(true);
    }    
}
