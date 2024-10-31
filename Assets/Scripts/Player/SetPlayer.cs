using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayer : MonoBehaviour
{
    public GameObject[] weaponList;

    private void OnEnable()
    {
        ActionManager.OnUpdateWeaponPlayer += UpdateWeaponEquip;
    }

    private void OnDisable()
    {
        ActionManager.OnUpdateWeaponPlayer -= UpdateWeaponEquip;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("WeaponId"))
        {
            weaponList[DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId)-1].SetActive(true);
        }
        else
        {
            weaponList[0].SetActive(true);
        }
    }

    private void UpdateWeaponEquip()
    {
        foreach (var t in weaponList)
        {
            t.SetActive(false);
        }
        weaponList[DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId)-1].SetActive(true);
    }
}
