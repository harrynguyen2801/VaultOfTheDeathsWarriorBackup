using System;
using System.Collections;
using System.Collections.Generic;
using CartoonHeroes;
using UnityEditor;
using UnityEngine;

public class PlayerModelEquipManager : MonoBehaviour
{
    public GameObject characterFashionMale;
    public GameObject characterFashionFemale;
    public GameObject characterMale;
    public GameObject characterFemale;
    private SetCharacter _setCharacter;
    private SetPlayer _setPlayerWeapon;
    private void Awake()
    {
        if (DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PlayerSex) == 1)
        {
            characterFemale.SetActive(true);
            _setCharacter = characterFashionFemale.GetComponent<SetCharacter>();
            _setPlayerWeapon = characterFashionFemale.GetComponent<SetPlayer>();
        }
        else
        {
            characterMale.SetActive(true);
            _setCharacter = characterFashionMale.GetComponent<SetCharacter>();
            _setPlayerWeapon = characterFashionMale.GetComponent<SetPlayer>();
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
        for (int i = 0; i < _setCharacter.itemGroups[itemGroupIdx].items.Length; i++)
        {
            DeactiveFashionItem(_setCharacter.itemGroups[itemGroupIdx], i);
        }
        ActiveFashionItem(_setCharacter.itemGroups[itemGroupIdx], fashionIdx);
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

        ActiveFashionItem(_setCharacter.itemGroups[(int)EnumManager.EFashionType.Hair-1],hair);
        ActiveFashionItem(_setCharacter.itemGroups[(int)EnumManager.EFashionType.Head-1],head);
        ActiveFashionItem(_setCharacter.itemGroups[(int)EnumManager.EFashionType.Torso-1],torso);
        ActiveFashionItem(_setCharacter.itemGroups[(int)EnumManager.EFashionType.Leg-1],leg);
    }
    
    public void ActiveFashionItem(SetCharacter.ItemGroup itemGroup, int itemSlot)
    {
        GameObject addedObj = _setCharacter.AddItem(itemGroup, itemSlot);
        Undo.RegisterCreatedObjectUndo(addedObj, "Added Item");
    }
    
    public void DeactiveFashionItem(SetCharacter.ItemGroup itemGroup, int itemSlot)
    {
        _setCharacter.RemoveObjFashionItems(itemGroup, itemSlot);
    }

    public void DeactiveAllFashionItemsInGroup(EnumManager.EFashionType eFashionType)
    {
        for (int i = 0; i < _setCharacter.itemGroups[(int)eFashionType-1].items.Length; i++)
        {
            DeactiveFashionItem(_setCharacter.itemGroups[(int)eFashionType-1], i);
        }
    }
}
