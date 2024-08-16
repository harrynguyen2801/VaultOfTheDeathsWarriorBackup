using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavContentWeapon : MonoBehaviour
{
    public GameObject content;

    public GameObject pbWeapon;

    public List<GameObject> listWeaponItem = new List<GameObject>();

    public void ShowWeaponList()
    {
        DataManager.Instance.LoadDataWeapon();
        listWeaponItem.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Debug.Log(content.transform.childCount);
            Destroy(content.transform.GetChild(i).gameObject);
        }
        foreach (var data in DataManager.Instance.weaponsData)
        {
            if (data.Value.Rest.Item1 == 1)
            {
                var weapon = Instantiate(pbWeapon,content.transform);
                weapon.GetComponent<WeaponItem>().SetDataWeapon(data.Key,data.Value);
                listWeaponItem.Add(weapon);
            }
        }
    }
    
    public void ShowWeaponListInventory()
    {
        DataManager.Instance.LoadDataWeapon();
        listWeaponItem.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        foreach (var data in DataManager.Instance.weaponsData)
        {
            if (data.Value.Rest.Item1 == 0)
            {
                var weapon = Instantiate(pbWeapon,content.transform);
                weapon.GetComponent<WeaponItem>().SetDataWeaponInventory(data.Key,data.Value);
                listWeaponItem.Add(weapon);
            }
        }
    }
}
