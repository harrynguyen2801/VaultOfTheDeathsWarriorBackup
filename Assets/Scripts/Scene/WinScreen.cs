using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public Image mainBgWin;
    public Image lineLeftWin;
    public Image lineRightWin;
    public Image[] star;
    
    private void WinScreenActive()
    {
        mainBgWin.DOFade(1f, .2f);
        mainBgWin.transform.DOScale(new Vector3(1f, 1f, 1f), .2f);
        for (int i = 0; i < star.Length; i++)
        {
            DOTween.Sequence().SetDelay(i/2f).Append(star[i].DOFade(1f, .2f));
            DOTween.Sequence().SetDelay(i/2f).Append(star[i].transform.DOMove(new Vector3(1f, 1f, 1f), .2f));
        }
        StartCoroutine(fillImagedWin());

    }

    private float durations = 1f;
    IEnumerator fillImagedWin()
    {
        float t = 0f;
        while (t < durations)
        {
            lineLeftWin.fillAmount = Mathf.Lerp(0f, 1f, t / durations);
            lineRightWin.fillAmount = Mathf.Lerp(0f, 1f, t / durations);
            t += Time.deltaTime;
            yield return null;
        }
    }
    private void OnEnable()
    {
        WinScreenActive();
    }
}
