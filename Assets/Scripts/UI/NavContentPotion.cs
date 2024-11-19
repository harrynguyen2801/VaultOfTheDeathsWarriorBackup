using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NavContentPotion : MonoBehaviour
{
    public GameObject content;

    public GameObject pbWeapon;

    public List<GameObject> listPotionItem = new List<GameObject>();

    //true is inventory, false is shop
    public bool shopOrInventory;

    private void OnEnable()
    {
        if (shopOrInventory)
        {
            ShowPotionListInventory();
        }
        else
        {
            ShowPotionListShop();
        }
    }

    public void ShowPotionListInventory()
    {
        DataManager.Instance.LoadDataPotion();
        listPotionItem.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Debug.Log(content.transform.childCount);
            Destroy(content.transform.GetChild(i).gameObject);
        }

        foreach (var data in DataManager.Instance.DataPotionPlayerBuy)
        {
            if (data.Value.Item2 != 0)
            {
                var potion = Instantiate(pbWeapon,content.transform);
                potion.GetComponent<PotionItem>().SetDataPotion(data.Key,DataManager.Instance.PotionsDataDefault[data.Key],shopOrInventory);
                potion.GetComponent<PotionItem>().ShowItem(0.1f);
                listPotionItem.Add(potion);
                Debug.Log(data.Value.Item2);
            }
        }
    }
    
    public void ShowPotionListShop()
    {
        listPotionItem.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        foreach (var data in DataManager.Instance.PotionsDataDefault)
        {
            var potion = Instantiate(pbWeapon,content.transform);
            potion.GetComponent<PotionItem>().SetDataPotion(data.Key,data.Value,shopOrInventory);
            potion.GetComponent<PotionItem>().ShowItem(0.1f);
            listPotionItem.Add(potion);
        }

        if (content.transform.childCount > 0)
        {
            content.transform.GetChild(0).GetComponent<PotionItem>().ChoosePotion();
        }
    }
}
