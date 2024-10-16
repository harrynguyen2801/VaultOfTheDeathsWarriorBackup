using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStartScene : MonoBehaviour
{
    public GameObject[] weaponList;
    private int _weaponIdPre = 1;
    private int _weaponIdCur = 1;

    private void Start()
    {
        if (PlayerPrefs.HasKey("WeaponId"))
        {
            weaponList[DataManager.Instance.GetDataInt(DataManager.EDataPrefName.WeaponId)-1].SetActive(true);
            _weaponIdPre = DataManager.Instance.GetDataInt(DataManager.EDataPrefName.WeaponId);
            _weaponIdCur = DataManager.Instance.GetDataInt(DataManager.EDataPrefName.WeaponId);
        }
        else
        {
            weaponList[0].SetActive(true);
        }
    }

    public void SetWeaponMeshRenderer(int weaponId)
    {
        _weaponIdPre = _weaponIdCur;
        _weaponIdCur = weaponId;
        weaponList[_weaponIdPre-1].SetActive(false);
        weaponList[_weaponIdCur-1].SetActive(true);
        DataManager.Instance.SaveData(DataManager.EDataPrefName.WeaponId,_weaponIdCur);
        // PlayerPrefs.SetInt("WeaponId",weaponIdCur);
        // PlayerPrefs.Save();
        Debug.Log(DataManager.Instance.GetDataInt(DataManager.EDataPrefName.WeaponId));
    }

    public void SetWeaponMeshRendererInventory(int weaponId)
    {
        _weaponIdPre = _weaponIdCur;
        _weaponIdCur = weaponId;
        weaponList[_weaponIdPre-1].SetActive(false);
        weaponList[_weaponIdCur-1].SetActive(true);
    }
}
