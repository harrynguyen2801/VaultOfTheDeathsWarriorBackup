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
        StartCoroutine(ActiveBtnWin());
    }

    public void LoseGame()
    {
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
        MainSceneManager.Instance.ShowNextLevel();
    }
    
    IEnumerator ActiveBtnWin()
    {
        winObj.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        bg.SetActive(true);
        yield return new WaitForSeconds(1.75f);
        btnHome.SetActive(true);
        btnNextLv.SetActive(true);
    }
    IEnumerator ActiveBtnLose()
    {
        loseObj.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        bg.SetActive(true);
        yield return new WaitForSeconds(1.75f);
        btnHome.SetActive(true);
    }
}
