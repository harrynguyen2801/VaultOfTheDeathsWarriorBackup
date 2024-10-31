using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class NavContentFashion : MonoBehaviour
{
    public GameObject content;

    public GameObject pbFashion;

    public List<GameObject> listFashionItem = new List<GameObject>();

    //true is inventory, false is shop
    public bool shopOrInventory;

    private string _sexPlayer;

    [FormerlySerializedAs("fashionType")] public EnumManager.EFashionType eFashionType;
    private void Start()
    {
        if (DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PlayerSex) == 1)
        {
            _sexPlayer = "Female";
        }
        else
        {
            _sexPlayer = "Male";
        }
        
        if (shopOrInventory)
        {
            ShowFashionListInventory(eFashionType);
        }
        else
        {
            ShowFashionListShop(eFashionType);
        }
    }

    public void ShowFashionListInventory(EnumManager.EFashionType type)
    {
        DataManager.Instance.LoadDataFashionWithType(type);
        listFashionItem.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Debug.Log(content.transform.childCount);
            Destroy(content.transform.GetChild(i).gameObject);
        }
        var data = DataManager.Instance.GetDictDataFashionWithType(type);
        for (int i = 0; i < data.Count; i++)
        {
            var dataItem = data.ElementAt(i);
            if (dataItem.Value.Item2 == 1)
            {
                var fashionItem = Instantiate(pbFashion,content.transform);
                fashionItem.GetComponent<FashionItem>().SetDataFashion(i, dataItem.Value, type,_sexPlayer);
                fashionItem.GetComponent<FashionItem>().ShowItem(0.1f);
                listFashionItem.Add(fashionItem);
            }
        }
    }
    
    public void ShowFashionListShop(EnumManager.EFashionType type)
    {
        DataManager.Instance.LoadDataFashionWithType(type);
        listFashionItem.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        
        var data = DataManager.Instance.GetDictDataFashionWithType(type);

        for (int i = 0; i < data.Count; i++)
        {
            var dataItem = data.ElementAt(i);
            if (dataItem.Value.Item2 == 0)
            {
                var fashionItem = Instantiate(pbFashion,content.transform);
                fashionItem.GetComponent<FashionItem>().SetDataFashion(i, dataItem.Value, type,_sexPlayer);
                fashionItem.GetComponent<FashionItem>().ShowItem(0.1f);
                listFashionItem.Add(fashionItem);
            }
        }

        if (content.transform.childCount == 0)
        {
            Debug.Log("You purchased all");
        }
    }
}
