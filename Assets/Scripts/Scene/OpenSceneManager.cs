using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSceneManager : MonoBehaviour
{
    public GameObject introScreen;
    public GameObject startScreen;
    public GameObject camera;
    void Awake()
    {
        Debug.Log("first game : "+PlayerPrefs.GetInt("FirstGame", 0));
        if (PlayerPrefs.GetInt("FirstGame", 0) == 0) 
        {
            PlayerPrefs.SetInt("FirstGame", 1);
            introScreen.SetActive(true);
            camera.SetActive(false);
        }
        else
        {
            camera.SetActive(true);
            startScreen.SetActive(true);
        }
    }
}
