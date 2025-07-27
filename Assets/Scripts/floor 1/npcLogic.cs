using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class npcLogic : MonoBehaviour
{
    public string currentDialogueText;
    public List<string> replics;

    private TMP_Text dialogueText;
    private int currentReplicNumber;

    public void Awake()
    {
        currentReplicNumber = 0;
        currentDialogueText = replics[currentReplicNumber];
        dialogueText = transform.Find("dialogue window").Find("text").GetComponent<TMP_Text>();
        changeDialogueText();
    }


    public void setDialogueWindowActive()
    {
        transform.Find("dialogue window").gameObject.SetActive(true);
    }

    public void nextReplic() // button
    {
        if (currentReplicNumber >= replics.Count - 1)
        {
            transform.Find("dialogue window").gameObject.SetActive(false);
            return;
        }
        currentReplicNumber += 1;
        changeDialogueText();
    }
    public void changeDialogueText()
    {
        currentDialogueText = replics[currentReplicNumber];
        dialogueText.text = currentDialogueText;
    }
}
