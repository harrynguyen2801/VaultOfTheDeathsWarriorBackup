using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailEnemyDescription : MonoBehaviour
{
    public TextMeshProUGUI tmpDetail;
    public TextMeshProUGUI tmpContent;
    public Image bg;
    public EnumManager.EGuideEnemy type;

    private void OnEnable()
    {
        ActionManager.OnUpdateHoverLevelScren += SetData;
    }

    private void OnDestroy()
    {
        ActionManager.OnUpdateHoverLevelScren -= SetData;
    }

    public void SetTypeEnemy(EnumManager.EGuideEnemy value)
    {
        type = value;
    }

    public void SetData()
    {
        var data = DataManager.Instance.GuideEnemyData.Single(data => data.Key == (int)type).Value;
        tmpDetail.text = data.Item1;
        tmpContent.text = data.Item3;
    }

    public void ActiveDetail()
    {
        ActionManager.OnUpdateHoverLevelScren.Invoke();
        bg.gameObject.SetActive(true);
        bg.DOFade(.85f, 0.3f);
        tmpDetail.DOFade(1f, 0.3f);
        tmpContent.DOFade(1f, 0.3f);
    }

    public void DeactiveDetail()
    {
        StartCoroutine(Deactive());
    }

    IEnumerator Deactive()
    {
        bg.DOFade(0f, 0.3f);
        tmpDetail.DOFade(0f, 0.3f);
        tmpContent.DOFade(0f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        bg.gameObject.SetActive(false);
    }
}
