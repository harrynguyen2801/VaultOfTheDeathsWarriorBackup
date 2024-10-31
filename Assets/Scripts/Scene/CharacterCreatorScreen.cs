using System;
using System.Collections;
using System.Collections.Generic;
using CartoonHeroes;
using UnityEditor;
using UnityEngine;

public class CharacterCreatorScreen : MonoBehaviour
{
    public NavContentFashion[] listNavContentFashions;
    
    private static CharacterCreatorScreen _instance;
    public static CharacterCreatorScreen Instance => _instance;

    public void ReloadNavContentFashionInventory(int navId)
    {
        listNavContentFashions[navId].ShowFashionListInventory(listNavContentFashions[navId].eFashionType);
    }
}
