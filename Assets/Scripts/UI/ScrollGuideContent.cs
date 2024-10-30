using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollGuideContent : MonoBehaviour
{
    public GuideItemScroll guideItemScroll;
    public GameObject contentParent;
    private int _eGuidePlayerCount;
    private int _eGuideEnemyCount;
    public List<GuideItemScroll> listGuideItemScrolls;

    private void Start()
    {
        _eGuideEnemyCount = Enum.GetValues(typeof(EnumManager.EGuideEnemy)).Length;
        _eGuidePlayerCount = Enum.GetValues(typeof(EnumManager.EGuidePlayer)).Length;
    }

    private void ClearDataContent()
    {
        for (int i = 0; i < contentParent.transform.childCount; i++)
        {
            Destroy(contentParent.transform.GetChild(i).gameObject);
        }
    }

    public void SetScrollGuideContent(EnumManager.EGuideType eGuideType)
    {
        ClearDataContent();
        listGuideItemScrolls.Clear();
        for (int i = 0; i < _eGuideEnemyCount; i++)
        {
            var i1 = i;
            var obj = Instantiate(guideItemScroll, contentParent.transform);
            obj.SetDataItem((EnumManager.EGuideEnemy)Enum.ToObject(typeof(EnumManager.EGuideEnemy),i1));
            listGuideItemScrolls.Add(obj);
        }
        // switch (eGuideType)
        // {
        //     case EnumManager.EGuideType.EGuideEnemy:
        //         // GuideScreen.Instance.tmpTitle.text = "Enemy";
        //         listGuideItemScrolls.Clear();
        //         for (int i = 0; i < _eGuideEnemyCount; i++)
        //         {
        //             var i1 = i;
        //             var obj = Instantiate(guideItemScroll, contentParent.transform);
        //             obj.SetDataItem((EnumManager.EGuideEnemy)Enum.ToObject(typeof(EnumManager.EGuideEnemy),i1));
        //             listGuideItemScrolls.Add(obj);
        //         }
        //         break;
        //     case EnumManager.EGuideType.EGuidePlayer:
        //         // GuideScreen.Instance.tmpTitle.text = "Player";
        //         listGuideItemScrolls.Clear();
        //         for (int i = 0; i < _eGuidePlayerCount; i++)
        //         {
        //             var i1 = i;
        //             var obj =  Instantiate(guideItemScroll, contentParent.transform);
        //             obj.SetDataItem((EnumManager.EGuidePlayer)Enum.ToObject(typeof(EnumManager.EGuideEnemy),i1));
        //             listGuideItemScrolls.Add(obj);
        //         }
        //         break;
        // }
    }
}
