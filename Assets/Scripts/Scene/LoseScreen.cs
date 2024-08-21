using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public Image lineLeftLose;
    public Image lineRightLose;    
    public Image mainBgLose;
    private float durations = 1f;

    private void LoseScreenActive()
    {
        mainBgLose.DOFade(1f, .2f);
        mainBgLose.transform.DOScale(new Vector3(1f, 1f, 1f), .2f);
        StartCoroutine(fillImagedLose());
    }

    IEnumerator fillImagedLose()
    {
        float t = 0f;
        while (t < durations)
        {
            lineLeftLose.fillAmount = Mathf.Lerp(0f, 1f, t / durations);
            lineRightLose.fillAmount = Mathf.Lerp(0f, 1f, t / durations);
            t += Time.deltaTime;
            yield return null;
        }
    }
    
    private void OnEnable()
    {
        LoseScreenActive();
    }
}
