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
        if (DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId) != 0)
        {
            UpdateWeaponEquip(true,DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId));
        }
        else
        {
            UpdateWeaponEquip(true,1);
        }
    }

    public void UpdateWeaponEquip(bool isInventory,int weaponId)
    {
        foreach (var t in weaponList)
        {
            t.SetActive(false);
        }

        if (isInventory)
        {
            weaponList[weaponId-1].SetActive(true);
            DataManager.Instance.SaveDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId,weaponId);
        }
        else
        {
            weaponList[weaponId-1].SetActive(true);
            if (DataManager.Instance.WeaponsDatas[weaponId].Rest.Item1 == 1)
            {
                DataManager.Instance.SaveDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId,weaponId);
            }
        }
    }
}
