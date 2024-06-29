using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStartScene : MonoBehaviour
{
    public GameObject[] weaponList;
    private int weaponIdPre = 1;
    private int weaponIdCur = 1;

    private void Start()
    {
        if (PlayerPrefs.HasKey("WeaponId"))
        {
            weaponList[DataManager.Instance.LoadDataInt(DataManager.dataName.WeaponId)-1].SetActive(true);
        }
        else
        {
            weaponList[0].SetActive(true);
        }
    }

    public void SetWeaponMeshRenderer(int weaponId)
    {
        weaponIdPre = weaponIdCur;
        weaponIdCur = weaponId;
        weaponList[weaponIdPre-1].SetActive(false);
        weaponList[weaponIdCur-1].SetActive(true);
        DataManager.Instance.SaveData(DataManager.dataName.WeaponId,weaponIdCur);
        // PlayerPrefs.SetInt("WeaponId",weaponIdCur);
        // PlayerPrefs.Save();
        Debug.Log(DataManager.Instance.LoadDataInt(DataManager.dataName.WeaponId));
    }
}
