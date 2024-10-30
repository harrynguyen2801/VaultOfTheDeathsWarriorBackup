using System;
using System.Collections;
using System.Collections.Generic;
using CartoonHeroes;
using UnityEditor;
using UnityEngine;

public class CharacterCreatorScreen : MonoBehaviour
{
    public GameObject characterMale;
    public GameObject characterFemale;

    public SetCharacter setCharacter;
    
    private static CharacterCreatorScreen _instance;
    public static CharacterCreatorScreen Instance => _instance;
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
        
        setCharacter = characterFemale.GetComponent<SetCharacter>();
    }

    private void Start()
    {
        if (DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PlayerSex) == 1)
        {
            setCharacter = characterFemale.GetComponent<SetCharacter>();
        }
        else
        {
            setCharacter = characterMale.GetComponent<SetCharacter>();
        }
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
}
