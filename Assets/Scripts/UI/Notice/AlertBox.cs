using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertBox : MonoBehaviour
{
    public Image imgBg;
    public Image imgHeader;
    public TextMeshProUGUI tmpHeader;
    public Image imgFadeLineTop;
    public Image imgFadeLineBot;
    public Image imgBtn;
    public TextMeshProUGUI tmpBtn;
    public TextMeshProUGUI tmpContent;
    public Image imgBtnClose;

    public void TweeningAppearAlertBox()
    {
        imgBg.DOFade(1f, 0.4f);
        imgHeader.DOFade(1f, 0.4f);
        tmpHeader.DOFade(1f, 0.4f);
        imgFadeLineTop.DOFade(1f, 0.4f);
        imgFadeLineBot.DOFade(1f, 0.4f);
        imgBtn.DOFade(1f, 0.4f);
        tmpBtn.DOFade(1f, 0.4f);
        tmpContent.DOFade(1f, 0.4f);
        imgBtnClose.DOFade(1f, 0.4f);
    }
    
    public void TweeningDeAppearAlertBox()
    {
        imgBg.DOFade(0f, 0.2f);
        imgHeader.DOFade(0f, 0.2f);
        tmpHeader.DOFade(0f, 0.2f);
        imgFadeLineTop.DOFade(0f, 0.2f);
        imgFadeLineBot.DOFade(0f, 0.2f);
        imgBtn.DOFade(0f, 0.2f);
        tmpBtn.DOFade(0f, 0.2f);
        tmpContent.DOFade(0f, 0.2f);
        imgBtnClose.DOFade(0f, 0.2f);
    }
}
