using System;
using System.Collections;
using System.Collections.Generic;
using CartoonHeroes;
using UnityEditor;
using UnityEngine;

public class CharacterModelShopManager : MonoBehaviour
{
    public GameObject characterFashionMale;
    public GameObject characterFashionFemale;
    public GameObject characterMale;
    public GameObject characterFemale;
    public SetCharacter setCharacter;
    
    private static CharacterModelShopManager _instance;
    public static CharacterModelShopManager Instance => _instance;
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
        
        if (DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PlayerSex) == 1)
        {
            characterFemale.SetActive(true);
            setCharacter = characterFashionFemale.GetComponent<SetCharacter>();
        }
        else
        {
            characterMale.SetActive(true);
            setCharacter = characterFashionMale.GetComponent<SetCharacter>();
        }
    }

    private void OnEnable()
    {
        ReloadFashionEquip();
        ActionManager.OnUpdateFashionPlayer += UpdateFashionEquip;
    }

    private void OnDisable()
    {        
        ActionManager.OnUpdateFashionPlayer -= UpdateFashionEquip;
    }

    private void UpdateFashionEquip(int itemGroupIdx, int fashionIdx)
    {
        for (int i = 0; i < setCharacter.itemGroups[itemGroupIdx].items.Length; i++)
        {
            DeactiveFashionItem(setCharacter.itemGroups[itemGroupIdx], i);
        }
        ActiveFashionItem(setCharacter.itemGroups[itemGroupIdx], fashionIdx);
    }

    private void ReloadFashionEquip()
    {
        int hair = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.Hair);
        int head = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.Head);
        int torso = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.Torso);
        int leg = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.Leg);

        DeactiveAllFashionItemsInGroup(EnumManager.EFashionType.Hair);
        DeactiveAllFashionItemsInGroup(EnumManager.EFashionType.Head);
        DeactiveAllFashionItemsInGroup(EnumManager.EFashionType.Torso);
        DeactiveAllFashionItemsInGroup(EnumManager.EFashionType.Leg);

        ActiveFashionItem(setCharacter.itemGroups[(int)EnumManager.EFashionType.Hair-1],hair);
        ActiveFashionItem(setCharacter.itemGroups[(int)EnumManager.EFashionType.Head-1],head);
        ActiveFashionItem(setCharacter.itemGroups[(int)EnumManager.EFashionType.Torso-1],torso);
        ActiveFashionItem(setCharacter.itemGroups[(int)EnumManager.EFashionType.Leg-1],leg);
    }
    
    public void ActiveFashionItem(SetCharacter.ItemGroup itemGroup, int itemSlot)
    {
        GameObject addedObj = setCharacter.AddItem(itemGroup, itemSlot);
        Undo.RegisterCreatedObjectUndo(addedObj, "Added Item");
    }
    
    public void DeactiveFashionItem(SetCharacter.ItemGroup itemGroup, int itemSlot)
    {
        setCharacter.RemoveObjFashionItems(itemGroup, itemSlot);
    }

    public void DeactiveAllFashionItemsInGroup(EnumManager.EFashionType eFashionType)
    {
        for (int i = 0; i < setCharacter.itemGroups[(int)eFashionType-1].items.Length; i++)
        {
            DeactiveFashionItem(setCharacter.itemGroups[(int)eFashionType-1], i);
        }
    }
}
