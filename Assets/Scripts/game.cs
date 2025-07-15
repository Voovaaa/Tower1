using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class game : MonoBehaviour
{

    public TMP_Text forTestText;
    private void Awake()
    {
    }
    private void Start()
    {
    }
    private void Update()
    {
        test.testTextUpdate(forTestText);
    }

}
