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
    public GameObject settingPopup;
    
    public Image titleGame;
    public TextMeshProUGUI clickToStartTmp;
    public TextMeshProUGUI settingTmp;
    public TextMeshProUGUI quitTmp;

    public Image imgLogo;
    public Image imgBg;
    
    private void TextToStartGame()
    {
        screen1.SetActive(false);
        screen2.SetActive(true);
        titleGame.DOFade(1f, 1.5f);
        titleGame.transform.DOLocalMoveY(180f, .75f);
        DOTween.Sequence().SetDelay(1.25f).Append(clickToStartTmp.DOFade(1f, 1.5f));
        DOTween.Sequence().SetDelay(1.5f).Append(settingTmp.DOFade(1f, 1.5f));
        DOTween.Sequence().SetDelay(1.75f).Append(quitTmp.DOFade(1f, 1.5f));

    }

    private void IntroStartGame()
    {
        StartCoroutine(IntroGame());
    }

    IEnumerator IntroGame()
    {
        screen1.SetActive(true);
        imgLogo.DOFade(1f, 1f);
        DOTween.Sequence().SetDelay(2f).Append(imgLogo.DOFade(0f, .75f));
        DOTween.Sequence().SetDelay(2.75f).Append(imgBg.DOFade(0f, .5f));
        yield return new WaitForSeconds(2.75f);
        TextToStartGame();
    }

    private void Start()
    {
        IntroStartGame();
        SoundManager.Instance.PlayBgm(EnumManager.EBgmSoundName.AssassinCreed);
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
}
