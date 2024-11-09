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
    public DetailEnemyDescription[] listEnemyDescriptions;
    public Image contentBg;
    public Image mapImg;

    public void SetLevelDetail(int lv)
    {
        var dataLevelDescription =
            DataManager.Instance.LevelDataDescriptions.Single(data => data.Key.Equals(lv)).Value;
        var dataEnemy = dataLevelDescription.Item4;

        listContentTmp[0].text = dataLevelDescription.Item1;
        listContentTmp[2].text = dataLevelDescription.Item2;
        mapImg.sprite = Resources.Load<Sprite>("MenuLevel/MapLevel/" + lv);
        Debug.Log("MenuLevel/MapLevel/" + lv);
        SetDataEnemyType(dataEnemy);
    }

    public void SetDataEnemyType(int[] dataEnemy)
    {
        for (int i = 0; i < dataEnemy.Length; i++)
        {
            listContentImg[i].gameObject.SetActive(true);
            listContentImg[i].sprite = Resources.Load<Sprite>("MenuLevel/Enemy/" + dataEnemy[i]);
            listEnemyDescriptions[i].SetTypeEnemy((EnumManager.EGuideEnemy)Enum.ToObject(typeof(EnumManager.EGuideEnemy),dataEnemy[i]));
        }
    }
}
