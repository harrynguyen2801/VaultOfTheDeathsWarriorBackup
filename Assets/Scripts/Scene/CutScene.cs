using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject TimeLine;
    public GameObject camera;

    private void OnEnable()
    {
        LoadingScreen.Instance.LoadScreen(StartScreen,TimeLine);     
        camera.SetActive(true);
    }
    

}
