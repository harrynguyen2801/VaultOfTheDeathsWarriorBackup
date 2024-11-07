using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PetMenuManager : MonoBehaviour
{
    public GameObject petDetailPanel;
    public GameObject swipeMenuPetPanel;
    public Anoucement anoucementPanel;
    public TutorialPetScreen tutorialPetScreen;

    public static PetMenuManager Instance => _instance;
    private static PetMenuManager _instance;
    
    public int petIndexCurrentActive;
    public int petIndex;

    public PetDetailManager petDetailManager;
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
    }

    private void Start()
    {
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Tutorial) == 0)
        {
            ActionManager.OnUpdateNextStepPetScreenTutorial?.Invoke(0);
        }
    }

    public void ShowPetDetailPanel(int indexPet)
    {
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Tutorial) == 0)
        {
            ActionManager.OnUpdateNextStepPetScreenTutorial?.Invoke(1);
        }
        petDetailPanel.SetActive(true);
        petIndex = indexPet;
        petIndexCurrentActive = (indexPet - 1) * 3;
        PetModelContainer.Instance.listPetModels[petIndexCurrentActive].SetActive(true);
        petDetailManager.SetDataPet(indexPet);
        petDetailManager.SetDataPetLevel((EnumManager.EPet)Enum.ToObject(typeof(EnumManager.EPet),indexPet));
    }

    public void ClosePetDetailPanel()
    {
        SwipeMenuPetItemManager.Instance.SetDataAllPetItem();
        petDetailPanel.SetActive(false);
        PetModelContainer.Instance.listPetModels[petIndexCurrentActive].SetActive(false);
    }

    public void ClickBtnBuy()
    {
        ClosePetDetailPanel();
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Tutorial) == 0)
        {
            tutorialPetScreen.HideTutorialBtn();
        }
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin) < DataManager.Instance.PetData[petIndex].Rest.Item1)
        {
            //TODO active anoucement
            ActionManager.OnUpdateAnoucement?.Invoke("You haven't enough coin to buy this pet.");
        }
        else
        {
            var coin = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin);
            coin -= DataManager.Instance.PetData[petIndex].Rest.Item1;
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.Coin,coin);
            ActionManager.OnUpdateCoin?.Invoke();
            ActionManager.OnOpenEggScreen?.Invoke(petIndex);
        }
    }
}
