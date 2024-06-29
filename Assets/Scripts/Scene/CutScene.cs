using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject TimeLine;

    private void OnEnable()
    {
        LoadingScreen.Instance.LoadScreen(StartScreen,TimeLine);
        // StartScreen.SetActive(true);
        // TimeLine.SetActive(false);
    }
    

}
