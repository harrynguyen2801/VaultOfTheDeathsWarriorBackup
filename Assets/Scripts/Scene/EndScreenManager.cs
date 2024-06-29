using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    public GameObject winObj;
    public GameObject loseObj;
    public GameObject btnHome;
    public GameObject btnNextLv;
    private void Start()
    {
        winObj.SetActive(true);
        StartCoroutine(ActiveBtn());
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
    
    IEnumerator ActiveBtn()
    {
        yield return new WaitForSeconds(2.25f);
        btnHome.SetActive(true);
        btnNextLv.SetActive(true);
    }
}
