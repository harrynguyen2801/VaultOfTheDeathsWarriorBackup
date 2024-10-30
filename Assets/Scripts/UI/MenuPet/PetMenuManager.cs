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

    public static PetMenuManager Instance => _instance;
    private static PetMenuManager _instance;
    
    public int petIndexCurrentActive;
    public int petIndex;

    public TextMeshProUGUI tmpCoin;

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
        tmpCoin.text = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin).ToString();
    }

    public void ShowPetDetailPanel(int indexPet)
    {
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
}
