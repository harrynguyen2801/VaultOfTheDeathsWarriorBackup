using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavContentWeapon : MonoBehaviour
{
    public GameObject content;

    public GameObject pbWeapon;

    public List<GameObject> listWeaponItem = new List<GameObject>();

    //true is inventory, false is shop
    public bool shopOrInventory;
    private void Start()
    {
        // if (shopOrInventory)
        // {
        //     ShowWeaponListInventory();
        // }
        // else
        // {
        //     ShowWeaponListShop();
        // }
    }

    private void OnEnable()
    {
        if (shopOrInventory)
        {
            ShowWeaponListInventory();
        }
        else
        {
            ShowWeaponListShop();
        }
    }

    public void ShowWeaponListInventory()
    {
        DataManager.Instance.LoadDataWeapon();
        listWeaponItem.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Debug.Log(content.transform.childCount);
            Destroy(content.transform.GetChild(i).gameObject);
        }

        foreach (var data in DataManager.Instance.WeaponsDatas)
        {
            if (data.Value.Rest.Item1 == 1)
            {
                var weapon = Instantiate(pbWeapon,content.transform);
                weapon.GetComponent<WeaponItem>().SetDataWeapon(data.Key,data.Value,shopOrInventory);
                weapon.GetComponent<WeaponItem>().ShowItem(0.1f);
                listWeaponItem.Add(weapon);
            }
        }


        int weaponIdEquip = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId);
        if (content.transform.childCount > 0 && weaponIdEquip != 0)
        {
            content.transform.GetChild(weaponIdEquip-1).GetComponent<WeaponItem>().ChooseWeapon();
        }
    }
    
    public void ShowWeaponListShop()
    {
        DataManager.Instance.LoadDataWeapon();
        listWeaponItem.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        foreach (var data in DataManager.Instance.WeaponsDatas)
        {
            if (data.Value.Rest.Item1 == 0)
            {
                var weapon = Instantiate(pbWeapon,content.transform);
                weapon.GetComponent<WeaponItem>().SetDataWeapon(data.Key,data.Value,shopOrInventory);
                weapon.GetComponent<WeaponItem>().ShowItem(0.1f);
                listWeaponItem.Add(weapon);
            }
        }
    }
}
