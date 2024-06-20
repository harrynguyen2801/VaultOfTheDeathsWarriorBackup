using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartGameScreen : MonoBehaviour
{
    public GameObject titleGame;
    public GameObject clickToStart;

    private void TextToStartGame()
    {
        LeanTween.alpha(titleGame.GetComponent<RectTransform>(), 1f, 1.5f);
        LeanTween.moveLocal(titleGame, new Vector3(-7f,31f,0f), 1f);
        LeanTween.alpha(clickToStart.GetComponent<RectTransform>(), 1f, 1.5f).setDelay(1.5f);
        LeanTween.scale(clickToStart, new Vector3(.9f, .9f, .9f), 1f).setLoopPingPong();
    }

    private void Start()
    {
        TextToStartGame();
    }
}
