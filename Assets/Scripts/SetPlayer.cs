using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayer : MonoBehaviour
{
    public GameObject[] weaponList;

    private void Start()
    {
        weaponList[PlayerPrefs.GetInt("WeaponId")-1].SetActive(true);
    }
}
