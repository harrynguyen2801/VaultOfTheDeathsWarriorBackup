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
        UpdateWeaponEquip();
    }

    private void UpdateWeaponEquip()
    {
        foreach (var t in weaponList)
        {
            t.SetActive(false);
        }

        if (DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId) != 0)
        {
            weaponList[DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId)-1].SetActive(true);
        }
    }
}
