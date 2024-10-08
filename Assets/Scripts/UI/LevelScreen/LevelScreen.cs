using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScreen : MonoBehaviour
{
    public static LevelScreen Instance => _instance;
    private static LevelScreen _instance;
    
    public LevelScreenItem[] listLevelScreenItems;

    public LevelContentDetail levelContentDetail;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        levelContentDetail = GetComponentInChildren<LevelContentDetail>();
    }

    private void Start()
    {
        SetOnclickItemLevelScreen();
        ShowALlItemLevel();
    }

    public void ShowALlItemLevel()
    {
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        for (int i = 0; i < listLevelScreenItems.Length; i++)
        {
            if (i % 2 == 0)
            {
                listLevelScreenItems[i].transform.localPosition = new Vector3(listLevelScreenItems[i].transform.localPosition.x, listLevelScreenItems[i].transform.localPosition.y - 80f, 0);
            }
            else
            {
                listLevelScreenItems[i].transform.localPosition = new Vector3(listLevelScreenItems[i].transform.localPosition.x, listLevelScreenItems[i].transform.localPosition.y + 80f, 0);
            }
            listLevelScreenItems[i].ShowItem(i/4f, listLevelScreenItems[i].transform.localPosition, i % 2 == 0);
        }
        yield return new WaitForSeconds(1f);
        listLevelScreenItems[0].GetComponent<Button>().onClick.Invoke();
    }

    private void SetOnclickItemLevelScreen()
    {
        for (int i = 0; i < listLevelScreenItems.Length; i++)
        {
            var i1 = i;
            listLevelScreenItems[i].GetComponent<Button>().onClick.AddListener(() => OnClickBtnLevelScreenItem(listLevelScreenItems[i1],i1));
        }
    }
    
    private void OnClickBtnLevelScreenItem(LevelScreenItem levelScreenItem, int lvIndex)
    {
        foreach (var item in listLevelScreenItems)
        {
            item.DeactiveHoverBtn();
        }
        levelScreenItem.ActiveHoverBtn();
        levelContentDetail.SetLevelDetail(lvIndex+1);
    }
}
