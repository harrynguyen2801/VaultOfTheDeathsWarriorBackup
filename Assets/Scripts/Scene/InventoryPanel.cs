using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryPanel : MonoBehaviour
{
    public InfomationTab InfomationTab;

    private static InventoryPanel _instance;
    public static InventoryPanel Instance => _instance;

    public Anoucement anoucement;

    public NavContentPet petsPanel;
    public NavContentWeapon weaponsPanel;
    public GameObject character3DModelPanel;
    public GameObject tutorialPanel;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialVillage) == 0)
        {
            tutorialPanel.SetActive(true);
        }
    }

    private void OnEnable()
    {
        UpdateNavPets();
        UpdateNavWeapons();
    }

    public void UpdateNavPets()
    {
        petsPanel.ShowPetListInventory();
    }
    
    public void UpdateNavWeapons()
    {
        weaponsPanel.ShowWeaponListInventory();
    }

    public void Active3DPanel()
    {
        character3DModelPanel.SetActive(true);
    }
    
    public void Deactive3DPanel()
    {
        character3DModelPanel.SetActive(false);
    }
}
