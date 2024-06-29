using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavContent : MonoBehaviour
{
    public GameObject content;

    public GameObject pbWeapon;

    public List<GameObject> listWeaponItem = new List<GameObject>();

    private void Start()
    {
        //ShowWeaponList();
    }

    public void ShowWeaponList()
    {
        listWeaponItem.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Debug.Log(content.transform.childCount);
            Destroy(content.transform.GetChild(i).gameObject);
        }
        foreach (var data in DataManager.Instance._weaponsData)
        {
            var weapon = Instantiate(pbWeapon,content.transform);
            weapon.GetComponent<WeaponItem>().SetDataWeapon(data.Key,data.Value);
            listWeaponItem.Add(weapon);
        }
    }
}
