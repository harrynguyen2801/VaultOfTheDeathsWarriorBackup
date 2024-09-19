using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class SettingTabManager : MonoBehaviour
{
    [SerializeField]
    private Button[] listBtnTab;
    [SerializeField]
    private TabSettingItem[] listTabSettingItems;
    
    //0 is GamePlay, 1 is Graphics, 2 is Sound
    [SerializeField]
    private GameObject[] listTabSettingContent;

    void Start()
    {
        Debug.Log(listTabSettingItems.Length + " | lengthtab");
        Debug.Log(listBtnTab.Length + " | length");
        for (int i = 0; i < listBtnTab.Length; i++)
        {
            var i1 = i;
            listBtnTab[i].onClick.AddListener(()=> SetActiveTabSettingItem(listTabSettingItems[i1]));
        }

        //set default screen is gameplay when open setting
        StartCoroutine(SetActiveDefaultTabSetting());
    }

    IEnumerator SetActiveDefaultTabSetting()
    {
        yield return new WaitForSeconds(0.9f);
        SetActiveTabSettingItem(listBtnTab[0].GetComponent<TabSettingItem>());
    }
    private void SetActiveTabSettingItem(TabSettingItem tabSettingItem)
    {
        DeactiveAllTabSetting();
        listTabSettingContent[(int)tabSettingItem.tabSettingType].SetActive(true);
        SetChoiceBtnEffect(tabSettingItem);
    }

    private void DeactiveAllTabSetting()
    {
        foreach (var item in listTabSettingContent)
        {
            item.gameObject.SetActive(false);
        }
    }
    private void SetChoiceBtnEffect(TabSettingItem tabSettingItem)
    {
        foreach (var item in listTabSettingItems)
        {
            item.SetImgBtnNormal();
        }
        tabSettingItem.SetImgBtnChoice();
    }
}
