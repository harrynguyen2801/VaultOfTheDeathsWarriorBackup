using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public GameObject lineLeftLose;
    public GameObject lineRightLose;    
    public GameObject mainBgLose;
    private float durations = 1f;

    private void LoseScreenActive()
    {
        LeanTween.alpha(mainBgLose.GetComponent<RectTransform>(), 1f, .2f);
        LeanTween.scale(mainBgLose, new Vector3(1f, 1f, 1f), .2f);
        StartCoroutine(fillImagedLose());
    }

    IEnumerator fillImagedLose()
    {
        float t = 0f;
        while (t < durations)
        {
            lineLeftLose.GetComponent<Image>().fillAmount = Mathf.Lerp(0f, 1f, t / durations);
            lineRightLose.GetComponent<Image>().fillAmount = Mathf.Lerp(0f, 1f, t / durations);
            t += Time.deltaTime;
            yield return null;
        }
    }
    
    private void OnEnable()
    {
        LoseScreenActive();
    }
}
