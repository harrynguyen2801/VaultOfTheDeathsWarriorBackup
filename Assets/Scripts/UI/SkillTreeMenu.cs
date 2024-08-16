using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeMenu : MonoBehaviour
{
    public Image treeImg;

    public CircleSkill skillCircle1;
    public CircleSkill skillCircle2;
    public CircleSkill skillCircle3;

    private float durations = 1f;

    private void Start()
    {
        StartCoroutine(fillImagedTree());
        StartCoroutine(DoFadeSkill());

    }

    IEnumerator DoFadeSkill()
    {
        yield return new WaitForSeconds(0.1f);
        skillCircle1.DoFadeCircleSkill();
        yield return new WaitForSeconds(0.2f);
        skillCircle2.DoFadeCircleSkill();
        yield return new WaitForSeconds(0.3f);
        skillCircle3.DoFadeCircleSkill();
    }

    IEnumerator fillImagedTree()
    {
        float t = 0f;
        while (t < durations)
        {
            treeImg.fillAmount = Mathf.Lerp(0f, 1f, t / durations);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
