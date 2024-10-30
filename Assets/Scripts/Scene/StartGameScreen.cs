using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGameScreen : MonoBehaviour
{
    public GameObject screen1;
    public GameObject screen2;
    
    public Image titleGame;
    
    private void Start()
    {
        IntroStartGame();
        SoundManager.Instance.PlayBgm(EnumManager.EBgmSoundName.AssassinCreed);
    }
    private void TextToStartGame()
    {
        screen2.SetActive(true);
        titleGame.DOFade(1f, 1f);
        titleGame.transform.DOLocalMoveY(180f, .5f);
    }

    private void IntroStartGame()
    {
        StartCoroutine(IntroGame());
    }
    IEnumerator IntroGame()
    {
        screen1.SetActive(true);
        yield return new WaitForSeconds(2.75f);
        TextToStartGame();
        yield return new WaitForSeconds(3.75f);
        screen1.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void NextScene()
    {
        LoadingScreen.Instance.LoadScene("StartScene");
    }

    public void OpenSetting()
    {
        GameManager.Instance.OpenSettingScreen();
    }
    
    public void OpenGuides()
    {
        GameManager.Instance.OpenGuideScreen();
    }
}
