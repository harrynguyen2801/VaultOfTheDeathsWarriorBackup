using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CircleSkill : MonoBehaviour
{
    public Image cirleImg;
    public Image wrapImg;
    public Image decorImg;
    public Image skillImg;

    public DataManager.ESkills eSkills;
    public void DoFadeCircleSkill()
    {
        cirleImg.DOFade(1f, .5f);
        decorImg.DOFade(1f, .5f);
        wrapImg.DOFade(1f, .5f);
        skillImg.DOFade(1f, .5f);
    }

    private void Start()
    {
        SetDataCK();
    }

    public void SetDataCK()
    {
        var idSkill = DataManager.Instance.GetUserSkill(eSkills);
        if (idSkill != 0)
        {
            skillImg.sprite = Resources.Load<Sprite>("Skills/" + eSkills + "/" + idSkill);
        }
        else
        {
            skillImg.sprite = Resources.Load<Sprite>("Skills/" + eSkills + "/" + 1);
        }
    }
}
