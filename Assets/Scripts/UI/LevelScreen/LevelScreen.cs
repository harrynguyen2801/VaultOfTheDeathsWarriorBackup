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

    private LevelContentDetail _levelContentDetail;
    
    public Button nextLevelButton;

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

        _levelContentDetail = GetComponentInChildren<LevelContentDetail>();
    }

    private void Start()
    {
        SetOnclickItemLevelScreen();
        ShowALlItemLevel();
        nextLevelButton.onClick.AddListener(VillageHomeScreen.Instance.LoadSceneMain);
        nextLevelButton.gameObject.SetActive(false);
    }

    public void ShowALlItemLevel()
    {
        DataManager.Instance.LoadDataLevelState();
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
            bool locked = DataManager.Instance.LevelStateData[i + 1].Item2 == 0;
            listLevelScreenItems[i].ShowItem(i/4f, listLevelScreenItems[i].transform.localPosition, i % 2 == 0,locked);

        }
        yield return new WaitForSeconds(1f);
        listLevelScreenItems[0].GetComponent<Button>().onClick.Invoke();
    }

    private void SetOnclickItemLevelScreen()
    {
        for (int i = 0; i < listLevelScreenItems.Length; i++)
        {
            var i1 = i;
            listLevelScreenItems[i].GetComponent<Button>().onClick.AddListener(() => OnClickBtnLevelScreenItem(listLevelScreenItems[i1],i1+1));
        }
    }
    
    private void OnClickBtnLevelScreenItem(LevelScreenItem levelScreenItem, int lvIndex)
    {
        CheckLevelOpen(levelScreenItem,lvIndex);
    }
    private void CheckLevelOpen(LevelScreenItem levelScreenItem, int lvIndex)
    {
        if (DataManager.Instance.LevelStateData[lvIndex].Item2 != 0)
        {
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.LevelPlay,lvIndex);
            Tuple<int, int> dataLevelStateNew = new Tuple<int, int>(DataManager.Instance.LevelStateData[lvIndex].Item1,1);
            DataManager.Instance.LevelStateData[lvIndex] = dataLevelStateNew;
            DataManager.Instance.SaveDataLevelState();
            SetDataOnclickItemLevelScreen(levelScreenItem,lvIndex);
            nextLevelButton.gameObject.SetActive(true);
        }
        else
        {
            ActionManager.OnUpdateAnoucement?.Invoke("Please Finish Previous Level To Unlock");
        }
    }
    
    private void SetDataOnclickItemLevelScreen(LevelScreenItem levelScreenItem, int lvIndex)
    {
        foreach (var item in listLevelScreenItems)
        {
            item.DeactiveHoverBtn();
        }
        levelScreenItem.ActiveHoverBtn();
        _levelContentDetail.SetLevelDetail(lvIndex);
    }
}
