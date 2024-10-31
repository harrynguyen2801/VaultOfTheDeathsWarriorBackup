using System;
using System.Collections;
using System.Collections.Generic;
using CartoonHeroes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class VillageHomeScreen : MonoBehaviour
{
    // public GameObject maleCharacter;
    // public GameObject femaleCharacter;
    // public SetPlayer weaponSetup;
    // public SetCharacter setCharacter;

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


    // private void OnEnable()
    // {
    //     ActionManager.OnUpdateFashionPlayer += UpdateFashionPlayer;
    // }
    //
    // private void OnDisable()
    // {
    //     ActionManager.OnUpdateFashionPlayer -= UpdateFashionPlayer;
    // }

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
        // if (DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PlayerSex) == 1)
        // {
        //     femaleCharacter.SetActive(true);
        //     weaponSetup = femaleCharacter.GetComponent<SetPlayer>();
        //     setCharacter = femaleCharacter.GetComponent<SetCharacter>();
        // }
        // else
        // {
        //     maleCharacter.SetActive(true);
        //     weaponSetup = maleCharacter.GetComponent<SetPlayer>();
        //     setCharacter = maleCharacter.GetComponent<SetCharacter>();
        // }
        //
        // ReloadFashionEquip();
        SoundManager.Instance.PlayBgm(EnumManager.EBgmSoundName.MildFlight);
    }

    // private void UpdateFashionPlayer(int itemGroupIdx, int fashionIdx)
    // {
    //     for (int i = 0; i < setCharacter.itemGroups[itemGroupIdx].items.Length; i++)
    //     {
    //         setCharacter.RemoveObjFashionItems(setCharacter.itemGroups[itemGroupIdx], i);
    //     }
    //     GameObject addedObj = setCharacter.AddItem(setCharacter.itemGroups[itemGroupIdx], fashionIdx);
    //     Undo.RegisterCreatedObjectUndo(addedObj, "Added Item");
    // }
    //
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
        character3DModelController.SetActive(false);
        dialoguePopup.SetActive(false);
    }

    public void OpenShopWithId(ShopId shopId)
    {
        listShop[(int)shopId].SetActive(true);
        character3DModelController.SetActive(true);
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
    
    
    //Test fashion default
    
    // private void ReloadFashionEquip()
    // {
    //     int hair = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.Hair);
    //     int head = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.Head);
    //     int torso = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.Torso);
    //     int leg = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.Leg);
    //
    //     DeactiveAllFashionItemsInGroup(EnumManager.EFashionType.Hair);
    //     DeactiveAllFashionItemsInGroup(EnumManager.EFashionType.Head);
    //     DeactiveAllFashionItemsInGroup(EnumManager.EFashionType.Torso);
    //     DeactiveAllFashionItemsInGroup(EnumManager.EFashionType.Leg);
    //
    //     ActiveFashionItem(setCharacter.itemGroups[(int)EnumManager.EFashionType.Hair-1],hair);
    //     ActiveFashionItem(setCharacter.itemGroups[(int)EnumManager.EFashionType.Head-1],head);
    //     ActiveFashionItem(setCharacter.itemGroups[(int)EnumManager.EFashionType.Torso-1],torso);
    //     ActiveFashionItem(setCharacter.itemGroups[(int)EnumManager.EFashionType.Leg-1],leg);
    // }
    // public void ActiveFashionItem(SetCharacter.ItemGroup itemGroup, int itemSlot)
    // {
    //     GameObject addedObj = setCharacter.AddItem(itemGroup, itemSlot);
    //     Undo.RegisterCreatedObjectUndo(addedObj, "Added Item");
    // }
    //
    // public void DeactiveFashionItem(SetCharacter.ItemGroup itemGroup, int itemSlot)
    // {
    //     setCharacter.RemoveObjFashionItems(itemGroup, itemSlot);
    // }
    //
    // public void DeactiveAllFashionItemsInGroup(EnumManager.EFashionType eFashionType)
    // {
    //     for (int i = 0; i < setCharacter.itemGroups[(int)eFashionType-1].items.Length; i++)
    //     {
    //         DeactiveFashionItem(setCharacter.itemGroups[(int)eFashionType-1], i);
    //     }
    // }
}
