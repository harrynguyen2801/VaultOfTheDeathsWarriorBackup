using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelContentDetail : MonoBehaviour
{
    public TextMeshProUGUI[] listContentTmp;
    public Image[] listContentImg;
    public Image decorBot;
    public Image decorTop;
    public Image contentBg;
    public Image mapImg;

    private void Start()
    {
        ShowSkillPanelDecor();
    }

    public void SetLevelDetail(int lv)
    {
        var dataLevelDescription =
            DataManager._instance.LevelDataDescriptions.Single(data => data.Key.Equals(lv)).Value;
        var dataEnemy = dataLevelDescription.Item4;

        listContentTmp[0].text = dataLevelDescription.Item1;
        listContentTmp[2].text = dataLevelDescription.Item2;
        AnimateImgDescription(mapImg,Resources.Load<Sprite>("MenuLevel/MapLevel/" + lv));
        
        DeactiveAllEnemyDescription();
        for (int i = 0; i < dataEnemy.Length; i++)
        {
            AnimateImgDescription(listContentImg[i],Resources.Load<Sprite>("WeaponSprites/" + dataEnemy[i]));
        }
    }

    private void AnimateImgDescription(Image img, Sprite sprite)
    {
        StartCoroutine(AnimateImg(img,sprite));
    }

    private void DeactiveAllEnemyDescription()
    {
        for (int i = 0; i < listContentImg.Length; i++)
        {
            listContentImg[i].DOFade(0f, 0.2f);
        }
    }

    IEnumerator AnimateImg(Image img, Sprite sprite)
    {
        img.DOFade(0f, 0.2f);
        yield return new WaitForSeconds(0.3f);
        img.sprite = sprite;
        img.DOFade(1f, 0.3f);
    }
    
    public void ShowSkillPanelDecor()
    {
        StartCoroutine(ShowContentPanel());
    }

    IEnumerator ShowContentPanel()
    {
        yield return new WaitForSeconds(1.25f);
        DOTween.Sequence().SetDelay(0f).Append(decorBot.transform.DOLocalMoveY(-40f,0.2f));
        DOTween.Sequence().SetDelay(0f).Append(decorTop.transform.DOLocalMoveY(12f,0.2f));
        
        for (int i = 0; i < listContentTmp.Length; i++)
        {
            listContentTmp[i].DOFade(1f, 0.3f);
        }
        contentBg.DOFade(0.65f, 0.3f);
        decorBot.DOFade(1f,0.3f);
        decorTop.DOFade(1f,0.3f);
        mapImg.DOFade(1f,0.3f);
    }
}
