using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuideScreen : MonoBehaviour
{
    public Image imgMainAction;
    public TextMeshProUGUI tmpNameMainAction;
    public TextMeshProUGUI tmpContentMainAction;
    // public TextMeshProUGUI tmpTitle;

    public TabGuideItem btnGuide;

    private ScrollGuideContent _scrollGuideContent;

    public static GuideScreen Instance => _instance;
    private static GuideScreen _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        _scrollGuideContent = GetComponentInChildren<ScrollGuideContent>();
    }

    private void Start()
    {
        TabGuideClick();
    }

    public void TabGuideClick()
    {
        EnumManager.EGuideType eGuideType = btnGuide.GetComponent<TabGuideItem>().tabSettingType;
        btnGuide.GetComponent<Button>().onClick.AddListener(() => BtnGuideClick(btnGuide,eGuideType));
    }

    public void BtnGuideClick(TabGuideItem tabSettingItem, EnumManager.EGuideType eGuideType)
    {
        _scrollGuideContent.SetScrollGuideContent(eGuideType);
        _scrollGuideContent.listGuideItemScrolls[0].GetComponent<Button>().onClick.Invoke();
    }

    public void ActiveGuideContent()
    {
        StartCoroutine(IEActiveGuideContent());
    }
    
    IEnumerator IEActiveGuideContent()
    {
        yield return new WaitForSeconds(0.1f);
        tmpNameMainAction.DOFade(1f, 0.2f);
        tmpContentMainAction.DOFade(1f, 0.2f);
        imgMainAction.DOFade(1f, 0.2f);
    }
    
    public void DeActiveGuideContent()
    {
        StartCoroutine(IEDeactiveGuideContent());
    }

    IEnumerator IEDeactiveGuideContent()
    {
        tmpNameMainAction.DOFade(0f, 0.2f);
        tmpContentMainAction.DOFade(0f, 0.2f);
        imgMainAction.DOFade(0f, 0.2f);
        yield return new WaitForSeconds(0.1f);
    }

    public void SetMainAction(EnumManager.EGuidePlayer eGuidePlayer, GuideItemScroll item)
    {
        DeActiveGuideContent();
        var nameSprite = DataManager.Instance.GuidePlayerData.Single(data => data.Key == (int)eGuidePlayer).Value.Item1;
        imgMainAction.sprite = Resources.Load<Sprite>("GuidePlayer/" + nameSprite);
        tmpNameMainAction.text = DataManager.Instance.GuidePlayerData.Single(data => data.Key == (int)eGuidePlayer).Value.Item2;
        tmpContentMainAction.text = DataManager.Instance.GuidePlayerData.Single(data => data.Key == (int)eGuidePlayer).Value.Item3;
        ActiveGuideContent();
    }
    
    public void SetMainAction(EnumManager.EGuideEnemy eGuideEnemy, GuideItemScroll item)
    {
        DeActiveGuideContent();
        var nameSprite = DataManager.Instance.GuideEnemyData.Single(data => data.Key == (int)eGuideEnemy).Value.Item1;
        imgMainAction.sprite = Resources.Load<Sprite>("GuideEnemy/" + nameSprite);
        tmpNameMainAction.text = DataManager.Instance.GuideEnemyData.Single(data => data.Key == (int)eGuideEnemy).Value.Item2;
        tmpContentMainAction.text = DataManager.Instance.GuideEnemyData.Single(data => data.Key == (int)eGuideEnemy).Value.Item3;
        ActiveGuideContent();
    }

    public void CloseGuideScreen()
    {
        gameObject.SetActive(false);
    }
}
