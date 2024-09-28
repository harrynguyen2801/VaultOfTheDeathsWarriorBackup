using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGuideItem : MonoBehaviour
{
    public EnumManager.EGuideType tabSettingType;
    public Sprite imgBtnChoice;
    public Sprite imgBtnNormal;
    public Image imgBtnBg;

    public void SetImgBtnNormal()
    {
        imgBtnBg.sprite = imgBtnNormal;
    }
    
    public void SetImgBtnChoice()
    {
        imgBtnBg.sprite = imgBtnChoice;
    }
}
