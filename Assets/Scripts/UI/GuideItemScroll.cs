using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuideItemScroll : MonoBehaviour
{
    public TextMeshProUGUI tmpMainText;
    public TextMeshProUGUI tmpDesciptionText;
    public EnumManager.EGuideType eGuideType;
    // public EnumManager.EGuidePlayer eGuidePlayer;
    public EnumManager.EGuideEnemy eGuideEnemy;
    public Button btnGuideItem;

    public Image elementalImg;
    // public void SetDataItem(EnumManager.EGuidePlayer _eGuidePlayer)
    // {
    //     tmpMainText.text = _eGuidePlayer.ToString();
    //     eGuidePlayer = _eGuidePlayer;
    //     btnGuideItem.onClick.AddListener(()=> GuideScreen.Instance.SetMainAction(eGuidePlayer,gameObject.GetComponent<GuideItemScroll>()));
    // }
    
    public void SetDataItem(EnumManager.EGuideEnemy _eGuideEnemy)
    {
        tmpMainText.text = _eGuideEnemy.ToString();
        eGuideEnemy = _eGuideEnemy;
        var idElemental = DataManager.Instance.GuideEnemyData.Single(data => data.Key == (int)eGuideEnemy).Value.Item4;
        elementalImg.sprite = Resources.Load<Sprite>("Element/" + idElemental);
        btnGuideItem.onClick.AddListener(()=> GuideScreen.Instance.SetMainAction(eGuideEnemy,gameObject.GetComponent<GuideItemScroll>()));
    }
}
