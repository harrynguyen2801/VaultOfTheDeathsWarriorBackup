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
        GameManager.Instance.ShowNextLevel(PlayerPrefs.GetInt("Level") + 1);
    }
    
    IEnumerator ActiveBtnWin()
    {
        yield return new WaitForSeconds(2.25f);
        btnHome.SetActive(true);
        btnNextLv.SetActive(true);
    }
    IEnumerator ActiveBtnLose()
    {
        yield return new WaitForSeconds(2.25f);
        btnHome.SetActive(true);
    }
}
