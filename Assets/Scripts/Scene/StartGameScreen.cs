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
    
    public GameObject titleGame;
    public TextMeshProUGUI clickToStartTmp;
    public TextMeshProUGUI settingTmp;
    public TextMeshProUGUI quitTmp;

    public GameObject imgLogo;
    public GameObject imgBg;


    private void TextToStartGame()
    {
        screen1.SetActive(false);
        screen2.SetActive(true);
        LeanTween.alpha(titleGame.GetComponent<RectTransform>(), 1f, 1.5f);
        LeanTween.moveLocal(titleGame, new Vector3(-552f,200f,0f), .75f);
        DOTween.Sequence().SetDelay(1.25f).Append(clickToStartTmp.DOFade(1f, 1.5f));
        DOTween.Sequence().SetDelay(1.5f).Append(settingTmp.DOFade(1f, 1.5f));
        DOTween.Sequence().SetDelay(1.75f).Append(quitTmp.DOFade(1f, 1.5f));

    }

    private void IntroStartGame()
    {
        Debug.Log("intro");
        StartCoroutine(IntroGame());
    }

    IEnumerator IntroGame()
    {
        screen1.SetActive(true);
        LeanTween.alpha(imgLogo.GetComponent<RectTransform>(), 1f, 1f);
        LeanTween.alpha(imgLogo.GetComponent<RectTransform>(), 0f, .75f).setDelay(2f);
        LeanTween.alpha(imgBg.GetComponent<RectTransform>(), 0f, .5f).setDelay(2.75f);
        yield return new WaitForSeconds(2.75f);
        TextToStartGame();
    }

    private void Start()
    {
        IntroStartGame();
        // TextToStartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSetting()
    {
        
    }

    public void CloseSetting()
    {
        
    }
}
