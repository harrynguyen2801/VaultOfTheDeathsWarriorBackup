using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSceneManager : MonoBehaviour
{
    public GameObject introScreen;
    public GameObject startScreen;
    public GameObject camera;
    public AudioSource musicBg;
    void Start()
    {
        if (PlayerPrefs.GetInt("FirstGame", 1) == 1) 
        {
            introScreen.SetActive(true);
            camera.SetActive(false);
            PlayerPrefs.SetInt("FirstGame", 0);
        }
        else
        {
            camera.SetActive(true);
            startScreen.SetActive(true);
        }
    }
}
