using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.UI.Dark;
using UnityEngine;
using UnityEngine.UI;

public class LevelScreen : MonoBehaviour
{
    public static LevelScreen Instance => _instance;
    private static LevelScreen _instance;
    
    public LevelScreenItem[] listLevelScreenItems;

    public LevelContentDetail levelContentDetail;
    
    public Button nextLevelButton;
    public ModalWindowManager modalWindowManager;
    public GameObject tutorialPanel;
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
    }

    private void Start()
    {
        ShowAllItemLevel();
        SetOnclickItemLevelScreen();
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialVillage) == 0)
        {
            tutorialPanel.SetActive(true);
        }
        nextLevelButton.onClick.AddListener(VillageHomeScreen.Instance.LoadSceneMain);
    }

    private void OnEnable()
    {
        ShowAllItemLevel();
    }

    public void ShowAllItemLevel()
    {
        DataManager.Instance?.LoadDataLevelState();
        for (int i = 0; i < listLevelScreenItems.Length; i++)
        {
            listLevelScreenItems[i].SetItemLevel(i+1);
        }
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
        Debug.Log("LevelOpen + " + lvIndex);
        CheckLevelOpen(levelScreenItem,lvIndex);
    }
    private void CheckLevelOpen(LevelScreenItem levelScreenItem, int lvIndex)
    {
        if (DataManager.Instance.LevelStateData[lvIndex].Item2 != 0)
        {
            modalWindowManager.ModalWindowIn();
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.LevelPlay,lvIndex);
            Tuple<int, int> dataLevelStateNew = new Tuple<int, int>(DataManager.Instance.LevelStateData[lvIndex].Item1,1);
            DataManager.Instance.LevelStateData[lvIndex] = dataLevelStateNew;
            DataManager.Instance.SaveDataLevelState();
            SetDataOnclickItemLevelScreen(lvIndex);
            nextLevelButton.gameObject.SetActive(true);
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.LevelPlay,lvIndex);
        }
        else if (lvIndex >= 3)
        {
            ActionManager.OnUpdateAnoucement?.Invoke("Level Is Coming Soon To Open");
        }
        else
        {
            ActionManager.OnUpdateAnoucement?.Invoke("Please Finish Previous Level To Unlock");
        }
    }
    
    private void SetDataOnclickItemLevelScreen(int lvIndex)
    {
        levelContentDetail.SetLevelDetail(lvIndex);
    }
}
