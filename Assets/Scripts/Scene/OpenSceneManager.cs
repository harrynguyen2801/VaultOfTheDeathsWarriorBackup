using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSceneManager : MonoBehaviour
{
    public GameObject introScreen;
    public GameObject startScreen;
    public AudioSource musicBg;
    void Start()
    {
        musicBg.Play(0);
        if (DataManager.Instance.LoadDataInt(DataManager.DataPrefName.FirstGame) == 0)
        {
            introScreen.SetActive(true);
            DataManager.Instance.SaveData(DataManager.DataPrefName.FirstGame,1);
        }
        else
        {
            startScreen.SetActive(true);
        }
    }
    
    public void NextScene()
    {
        Debug.Log("click");
            LoadingScreen.Instance.LoadScene("StartScene");
    }
}
