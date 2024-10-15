using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeMenu : MonoBehaviour
{
    public Image treeImg;
    public Image decorFg;
    public Image imgDecorArrow;
    public Image imgDecorCircle;

    public CircleSkill skillCircle1;
    public CircleSkill skillCircle2;
    public CircleSkill skillCircle3;

    private float durations = 1f;

    private void OnEnable()
    {
        StartCoroutine(fillImagedTree());
        StartCoroutine(DoFadeUIActive());
        StartCoroutine(InvokeBtnDefault());
    }

    IEnumerator InvokeBtnDefault()
    {
        yield return new WaitForSeconds(.5f);
        SkillsScreenManager.Instance.btnSelected = SkillsScreenManager.Instance.skillCircleList[0].gameObject;
        SkillsScreenManager.Instance.skillCircleList[0].onClick.Invoke();
    }

    IEnumerator DoFadeUIActive()
    {
        yield return new WaitForSeconds(0.1f);
        skillCircle1.DoFadeCircleSkill(1f,0.5f);
        yield return new WaitForSeconds(0.2f);
        skillCircle2.DoFadeCircleSkill(1f,0.5f);
        yield return new WaitForSeconds(0.3f);
        imgDecorArrow.DOFade(1f, 0.5f);
        imgDecorCircle.DOFade(1f, 0.5f);
        skillCircle3.DoFadeCircleSkill(1f,0.5f);
    }
    
    private void DoFadeUIDeActive()
    {
        skillCircle1.DoFadeCircleSkill(0f,0.1f);
        skillCircle2.DoFadeCircleSkill(0f,0.1f);
        imgDecorArrow.DOFade(0f,0.1f);
        imgDecorCircle.DOFade(0f,0.1f);
        skillCircle3.DoFadeCircleSkill(0f,0.1f);
        treeImg.fillAmount = 0f;
    }

    IEnumerator fillImagedTree()
    {
        float t = 0f;
        while (t < durations)
        {
            if (t < durations/2)
            {
                decorFg.fillAmount = Mathf.Lerp(0f, 1f, t / (durations/2));
            }
            treeImg.fillAmount = Mathf.Lerp(0f, 1f, t / durations);
            t += Time.deltaTime;
            yield return null;
        }
    }

    private void OnDisable()
    {
        DoFadeUIDeActive();
    }
}
