using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CartoonHeroes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class VillageHomeScreen : MonoBehaviour
{
    private static VillageHomeScreen _instance;
    public static VillageHomeScreen Instance => _instance;

    public enum ShopId : int
    {
        WeaponShop = 0,
        SkillsShop = 1,
        GroceryShop = 2,
        FashionShop = 5,
    }
    public GameObject[] listShop;

    public GameObject dialoguePopup;
    public GameObject character3DModelController;
    public PlayerModelEquipManager playerModelEquipManager;

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

    void Start()
    {
        SoundManager.Instance.PlayBgm(EnumManager.EBgmSoundName.MildFlight);

        DataManager.Instance.LoadDictDataPet();
        var petId = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PetId);
        if (petId != 0)
        {
            var petLv = DataManager.Instance.PetData[petId].Item2;
            Debug.Log("village : pet id " + petId + " pet lv " + petLv);
            ActionManager.OnUpdatePetInventoryModelView?.Invoke(petId,petLv);
        }
    }
    public void LoadSceneMain()
    {
        LoadingScreen.Instance.LoadScene("MainScene");
    }
    
    public void OpenLevelScreen()
    {
        listShop[3].SetActive(true);
    }
    
    public void OpenPersonalScreen()
    {
        character3DModelController.SetActive(true);
        listShop[4].SetActive(true);
    }
    
    public void CloseAllChildScreens()
    {
        foreach (var shop in listShop)
        {
            shop.SetActive(false);
        }
        playerModelEquipManager.ReloadFashionEquip();
        character3DModelController.SetActive(false);
        dialoguePopup.SetActive(false);
    }

    public void OpenShopWithId(ShopId shopId)
    {
        listShop[(int)shopId].SetActive(true);
        switch ((int)shopId)
        {
            case 0:
                character3DModelController.SetActive(true);
                break;
            case 5:
                character3DModelController.SetActive(true);
                break;
        }
    }
    public void ActiveDialoguePopup(int idNpc)
    {
        dialoguePopup.SetActive(true);
        DialogueManager dialogueManager = dialoguePopup.GetComponent<DialogueManager>();
        dialogueManager.SetDataDialogue(DataManager.Instance.GetNpcDataByID(idNpc));
        dialogueManager.ActiveDialogue();
        dialogueManager.SetButtonFunc(idNpc);
    }
    
    public void OpenSetting()
    {
        GameManager.Instance.OpenSettingScreen();
    }
    
    public void OpenGuide()
    {
        GameManager.Instance.OpenGuideScreen();
    }
}
