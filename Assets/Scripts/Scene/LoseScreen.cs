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
    private float durations = .65f;

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
    
    IEnumerator ActiveLoseScreen(string animName)
    {
        Animator anim = GetComponent<Animator>();
        AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(0);
        while (animState.IsName(animName) && animState.normalizedTime >= 1.0f)
        {
            animState = anim.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        yield return new WaitForSeconds(.75f);
        anim.SetTrigger("Trans");
        yield return new WaitForSeconds(.3f);
        LoseScreenActive();
    }
    private void OnEnable()
    {
        // LoseScreenActive();
        StartCoroutine(ActiveLoseScreen("BGDissolveOut"));
    }
}
