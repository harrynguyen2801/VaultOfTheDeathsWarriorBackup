using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject settingScreen;
    [SerializeField]
    private GameObject guideScreen;
    
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    public void OpenSettingScreen()
    {
        settingScreen.GetComponent<SettingManager>().OpenSettingTab();
    }
    public void OpenGuideScreen()
    {
        guideScreen.GetComponent<GuideManager>().OpenGuideTab();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenSettingScreen();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenGuideScreen();
        }
    }
}
