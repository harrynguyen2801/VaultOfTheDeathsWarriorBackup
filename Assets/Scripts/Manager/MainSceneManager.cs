using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager Instance => _instance;
    private static MainSceneManager _instance;
    public GameObject profile;

    public GameObject[] levelList;
    public GameObject player;
    public GameObject enemySpawn;
    public EndScreenManager endingScreen;

    public bool winOrLose;

    private void Awake()
    {
        ShowCurrentLevel();
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
        profile.SetActive(true);
    }

    private void ShowCurrentLevel()
    {
        SoundManager.Instance.PlayBgm(EnumManager.EBgmSoundName.DungeonLoop);
        int level = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.LevelPlay);
        Debug.Log(level + " level");
        if (PlayerPrefs.HasKey("LevelPlay"))
        {
            levelList[level-1].gameObject.SetActive(true);
            player.GetComponent<Transform>().position = levelList[level - 1].GetComponent<GameLevelManager>().playerStartPosition.position;
        }
        else
        {
            levelList[0].gameObject.SetActive(true);
            player.GetComponent<Transform>().position = levelList[0].GetComponent<GameLevelManager>().playerStartPosition.position;
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.Level,1);
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.LevelPlay,1);
        }
    }

    public void ShowNextLevel()
    {
        int level = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.LevelPlay);
        levelList[level-1].gameObject.SetActive(false);
        levelList[level].gameObject.SetActive(true);
        player.GetComponent<Transform>().position = levelList[level].GetComponent<GameLevelManager>().playerStartPosition.position;
        StartCoroutine(waitSecond(3f));
        player.GetComponent<Player>().AppearPlayerInGame();
    }
    
    IEnumerator waitSecond(float sec)
    {
        yield return new WaitForSeconds(sec);
    }
    
    public void OpenSetting()
    {
        GameManager.Instance.OpenSettingScreen();
    }
}
