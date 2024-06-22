using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public GameObject mainBgWin;
    public GameObject lineLeftWin;
    public GameObject lineRightWin;
    public GameObject[] star;



    private void WinScreenActive()
    {
        LeanTween.alpha(mainBgWin.GetComponent<RectTransform>(), 1f, .2f);
        LeanTween.scale(mainBgWin, new Vector3(1f, 1f, 1f), .2f);
        for (int i = 0; i < star.Length; i++)
        {
            LeanTween.alpha(star[i].GetComponent<RectTransform>(), 1f, .2f).setDelay(i/2f);
            LeanTween.scale(star[i], new Vector3(1f, 1f, 1f), .2f).setDelay(i/2f);
        }
        StartCoroutine(fillImagedWin());

    }

    private float durations = 1f;
    IEnumerator fillImagedWin()
    {
        float t = 0f;
        while (t < durations)
        {
            lineLeftWin.GetComponent<Image>().fillAmount = Mathf.Lerp(0f, 1f, t / durations);
            lineRightWin.GetComponent<Image>().fillAmount = Mathf.Lerp(0f, 1f, t / durations);
            t += Time.deltaTime;
            yield return null;
        }
    }
    private void OnEnable()
    {
        WinScreenActive();
    }
}
