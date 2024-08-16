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
        // musicBg.Play(0);
        if (DataManager.Instance.GetDataInt(DataManager.EDataPrefName.FirstGame) == 0)
        {
            introScreen.SetActive(true);
            camera.SetActive(false);
            DataManager.Instance.SaveData(DataManager.EDataPrefName.FirstGame,1);
        }
        else
        {
            camera.SetActive(true);
            startScreen.SetActive(true);
        }
    }
    
    public void NextScene()
    {
        Debug.Log("click");
            LoadingScreen.Instance.LoadScene("StartScene");
    }
}
