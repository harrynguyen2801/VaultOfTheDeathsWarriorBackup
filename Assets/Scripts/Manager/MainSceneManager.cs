using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager Instance => _instance;
    private static MainSceneManager _instance;
    public GameObject profile;

    public GameObject[] levelList;
    public GameObject player;
    public GameObject pet3DController;
    public EndScreenManager endingScreen;

    public bool winOrLose;
    public GameObject tutorialPanel;

    public CinemachineVirtualCamera virtualCamera;
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
        profile.SetActive(true);
        ShowCurrentLevel();
    }

    private void ShowCurrentLevel()
    {
        SoundManager.Instance.PlayBgm(EnumManager.EBgmSoundName.DungeonLoop);
        int level = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.LevelPlay);
        Debug.Log(level + " level");
        if (PlayerPrefs.HasKey("LevelPlay"))
        {
            levelList[level-1].gameObject.SetActive(true);
            player.transform.position = levelList[level - 1].GetComponent<GameLevelManager>().playerStartPosition.position;
            pet3DController.transform.position = levelList[level - 1].GetComponent<GameLevelManager>().playerStartPosition.position;
        }
        else
        {
            levelList[0].gameObject.SetActive(true);
            tutorialPanel.SetActive(true);
            player.transform.position = levelList[0].GetComponent<GameLevelManager>().playerStartPosition.position;
            pet3DController.transform.position = levelList[0].GetComponent<GameLevelManager>().playerStartPosition.position;
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.LevelOpen,1);
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.LevelPlay,1);
        }
    }

    public void ShowNextLevel()
    {
        int level = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.LevelPlay);
        Debug.Log("Current LevelOpen: " + level);

        if (level < 1 || level > levelList.Length)
        {
            Debug.LogError("Invalid level index: " + level);
            return;
        }

        levelList[level - 1].gameObject.SetActive(false);
        levelList[level].gameObject.SetActive(true);
        DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.LevelPlay,level + 1);

        GameLevelManager levelManager = levelList[level].GetComponent<GameLevelManager>();
        if (levelManager == null || levelManager.playerStartPosition == null)
        {
            Debug.LogError("GameLevelManager or playerStartPosition is missing in level " + level);
            return;
        }

        Vector3 startPosition = levelManager.playerStartPosition.position;
        player.transform.position = startPosition;
        pet3DController.transform.position = startPosition;

        Debug.Log("Player and pet positions set to: " + startPosition);

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
    
    public void OpenPausePopup()
    {
        GameManager.Instance.OpenPausePopup();
    }
    
    public void OpenGuides()
    {
        GameManager.Instance.OpenGuideScreen();
    }

    public void ZoomOutCamera()
    {
        StartCoroutine(LerpFOV(90));
        virtualCamera.transform.rotation = Quaternion.Euler(55, -45, 0);
    }

    public void ZoomInCamera()
    {
        StartCoroutine(LerpFOV(60));
        virtualCamera.transform.rotation = Quaternion.Euler(45, -45, 0);
    }

    IEnumerator LerpFOV(float valueLerp)
    {
        float lerpDuration = 1.75f;
        float timeCur = 0;
        while (timeCur < lerpDuration)
        {
            timeCur += Time.deltaTime;
            float lerpValue = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView,valueLerp,timeCur/lerpDuration);
            virtualCamera.m_Lens.FieldOfView = lerpValue;
            yield return null;
        }
    }
}
