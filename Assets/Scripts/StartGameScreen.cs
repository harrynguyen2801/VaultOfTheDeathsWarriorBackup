using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGameScreen : MonoBehaviour
{
    public GameObject screen1;
    public GameObject screen2;

    
    public GameObject titleGame;
    public GameObject clickToStart;
    
    public GameObject imgLogo;
    public GameObject imgBg;


    private void TextToStartGame()
    {
        screen1.SetActive(false);
        screen2.SetActive(true);
        LeanTween.alpha(titleGame.GetComponent<RectTransform>(), 1f, 1.5f);
        LeanTween.moveLocal(titleGame, new Vector3(0f,31f,0f), 1f);
        LeanTween.alpha(clickToStart.GetComponent<RectTransform>(), 1f, 1.5f).setDelay(1.5f);
        LeanTween.scale(clickToStart, new Vector3(.9f, .9f, .9f), 1f).setLoopPingPong();
    }

    private void IntroStartGame()
    {
        Debug.Log("intro");
        StartCoroutine(IntroGame());
    }

    IEnumerator IntroGame()
    {
        screen1.SetActive(true);
        LeanTween.alpha(imgLogo.GetComponent<RectTransform>(), 1f, 1.5f);
        LeanTween.alpha(imgLogo.GetComponent<RectTransform>(), 0f, .5f).setDelay(2.25f);
        LeanTween.alpha(imgBg.GetComponent<RectTransform>(), 0f, .5f).setDelay(2.25f);
        yield return new WaitForSeconds(2.25f);
        TextToStartGame();
    }

    private void Start()
    {
        IntroStartGame();
        // TextToStartGame();
    }
}
