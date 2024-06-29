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
        LeanTween.alpha(titleGame.GetComponent<RectTransform>(), 1f, 1.25f);
        LeanTween.moveLocal(titleGame, new Vector3(0f,31f,0f), .75f);
        LeanTween.alpha(clickToStart.GetComponent<RectTransform>(), 1f, 1.25f).setDelay(1.25f);
        LeanTween.scale(clickToStart, new Vector3(.9f, .9f, .9f), .75f).setLoopPingPong();
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
}
