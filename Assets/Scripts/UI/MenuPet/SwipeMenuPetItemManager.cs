using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMenuPetItemManager : MonoBehaviour
{
    public List<SwipeMenuPetItem> listPetItems = new List<SwipeMenuPetItem>();
    public static SwipeMenuPetItemManager Instance => _instance;
    private static SwipeMenuPetItemManager _instance;

    public GameObject contentParent;
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

    public void SetDataPetItem()
    {
        var data = DataManager.Instance.PetData;
        for (int i = 0; i < listPetItems.Count; i++)
        {
            listPetItems[i].SetupItem((EnumManager.EPet)Enum.ToObject(typeof(EnumManager.EPet),i+1));
        }
    }

    public void SetDataAllPetItem()
    {
        var data = DataManager.Instance.PetData;
        for (int i = 0; i < contentParent.transform.childCount; i++)
        {
            contentParent.transform.GetChild(i).GetComponent<SwipeMenuPetItem>().SetupItem();
        }
    }
}
