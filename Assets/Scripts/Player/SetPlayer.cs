using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayer : MonoBehaviour
{
    public GameObject[] weaponList;

    private void Start()
    {
        if (PlayerPrefs.HasKey("WeaponId"))
        {
            weaponList[DataManager.Instance.LoadDataInt(DataManager.DataPrefName.WeaponId)-1].SetActive(true);
        }
        else
        {
            weaponList[0].SetActive(true);
        }
    }
}
