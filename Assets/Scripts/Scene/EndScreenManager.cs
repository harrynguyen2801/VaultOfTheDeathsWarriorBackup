using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    public GameObject bg;
    public GameObject winObj;
    public GameObject loseObj;
    public GameObject btnHome;
    public GameObject btnNextLv;
    public void WinGame()
    {
        bg.SetActive(true);
        winObj.SetActive(true);
        StartCoroutine(ActiveBtnWin());
    }

    public void LoseGame()
    {
        bg.SetActive(true);
        loseObj.SetActive(true);
        StartCoroutine(ActiveBtnLose());
    }
    
    public void ReturnHome()
    {
        gameObject.SetActive(false);
        LoadingScreen.Instance.LoadScene("StartScene");
    }

    public void NextLevel()
    {
        gameObject.SetActive(false);
        MainSceneManager.Instance.ShowNextLevel(DataManager.Instance.GetDataInt(DataManager.DataPrefName.Level) + 1);
    }
    
    IEnumerator ActiveBtnWin()
    {
        yield return new WaitForSeconds(1.75f);
        btnHome.SetActive(true);
        btnNextLv.SetActive(true);
    }
    IEnumerator ActiveBtnLose()
    {
        yield return new WaitForSeconds(1.75f);
        btnHome.SetActive(true);
    }
}
