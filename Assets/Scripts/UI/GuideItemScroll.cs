using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuideItemScroll : MonoBehaviour
{
    public TextMeshProUGUI tmpMainText;
    public EnumManager.EGuideType eGuideType;
    public EnumManager.EGuidePlayer eGuidePlayer;
    public EnumManager.EGuideEnemy eGuideEnemy;
    public Button btnGuideItem;
    
    public Sprite imgBtnChoice;
    public Sprite imgBtnNormal;
    public Image imgBtnBg;

    public void SetDataItem(EnumManager.EGuidePlayer _eGuidePlayer)
    {
        tmpMainText.text = _eGuidePlayer.ToString();
        eGuidePlayer = _eGuidePlayer;
        btnGuideItem.onClick.AddListener(()=> GuideScreen.Instance.SetMainAction(eGuidePlayer,gameObject.GetComponent<GuideItemScroll>()));
    }
    
    public void SetDataItem(EnumManager.EGuideEnemy _eGuideEnemy)
    {
        tmpMainText.text = _eGuideEnemy.ToString();
        eGuideEnemy = _eGuideEnemy;
        btnGuideItem.onClick.AddListener(()=> GuideScreen.Instance.SetMainAction(eGuideEnemy,gameObject.GetComponent<GuideItemScroll>()));
    }
    
    public void SetImgBtnNormal()
    {
        imgBtnBg.sprite = imgBtnNormal;
    }
    
    public void SetImgBtnChoice()
    {
        imgBtnBg.sprite = imgBtnChoice;
    }
}
