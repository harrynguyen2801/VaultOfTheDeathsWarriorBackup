using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private Image imgBgSetting;

    [SerializeField] private Image imgBgBtnGameplay;
    [SerializeField] private Image imgBgBtnGraphics;
    [SerializeField] private Image imgBgSound;
    
    [SerializeField] private Image imgArrowBtnGameplay;
    [SerializeField] private Image imgArrowBtnGraphics;
    [SerializeField] private Image imgArrowSound;
    
    [SerializeField] private TextMeshProUGUI tmpBtnGameplay;
    [SerializeField] private TextMeshProUGUI tmpBtnGraphics;
    [SerializeField] private TextMeshProUGUI tmpSound;

    [SerializeField] private Button btnApply;
    [SerializeField] private TextMeshProUGUI tmpBtnApply;
    [SerializeField] private Button btnClose;
    [SerializeField] private Image imgXBtnClose;

    private void OnEnable()
    {
        StartCoroutine(SettingActiveTween());
    }

    private void Start()
    {
        btnClose.onClick.AddListener(CloseSettingScreen);
        btnApply.onClick.AddListener(SaveSettingScreen);
    }

    IEnumerator SettingActiveTween()
    {
        Debug.Log("active setting");
        imgBgSetting.DOFade(1f, 0.3f);
        
        yield return new WaitForSeconds(0.2f);
        
        imgBgBtnGameplay.DOFade(1f, 0.3f);
        imgArrowBtnGameplay.DOFade(1f, 0.3f);
        tmpBtnGameplay.DOFade(1f, 0.3f);
        
        imgBgBtnGraphics.DOFade(1f, 0.4f);
        tmpBtnGraphics.DOFade(1f, 0.4f);
        imgArrowBtnGraphics.DOFade(1f, 0.4f);
        
        imgBgSound.DOFade(1f, 0.5f);
        tmpSound.DOFade(1f, 0.5f);
        imgArrowSound.DOFade(1f, 0.5f);
        
        yield return new WaitForSeconds(0.2f);
        
        btnApply.GetComponent<Image>().DOFade(1f, 0.45f);
        tmpBtnApply.DOFade(1f, 0.45f);
        
        imgXBtnClose.DOFade(1f, 0.45f);
        btnClose.GetComponent<Image>().DOFade(1f, 0.45f);
    }
    
    IEnumerator SettingDeActiveTween()
    {
        Debug.Log("Deactive setting");
        imgBgSetting.DOFade(0f, 0.3f);
        imgBgBtnGameplay.DOFade(0f, 0.3f);
        imgArrowBtnGameplay.DOFade(0f, 0.3f);
        tmpBtnGameplay.DOFade(0f, 0.3f);
        
        imgBgBtnGraphics.DOFade(0f, 0.4f);
        tmpBtnGraphics.DOFade(0f, 0.4f);
        imgArrowBtnGraphics.DOFade(0f, 0.4f);
        
        imgBgSound.DOFade(0f, 0.5f);
        tmpSound.DOFade(0f, 0.5f);
        imgArrowSound.DOFade(0f, 0.5f);
        btnApply.GetComponent<Image>().DOFade(0f, 0.45f);
        tmpBtnApply.DOFade(0f, 0.45f);
        
        imgXBtnClose.DOFade(0f, 0.45f);
        btnClose.GetComponent<Image>().DOFade(0f, 0.45f);
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    private void CloseSettingScreen()
    {
        StartCoroutine(SettingDeActiveTween());
    }
    
    private void SaveSettingScreen()
    {
        StartCoroutine(SettingDeActiveTween());
    }
}
