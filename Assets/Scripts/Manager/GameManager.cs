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
        settingScreen.SetActive(true);
    }
    
    public void OpenGuideScreen()
    {
        guideScreen.SetActive(true);
    }
}
