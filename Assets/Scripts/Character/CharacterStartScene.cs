using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStartScene : MonoBehaviour
{
    public GameObject[] weaponList;
    private int _weaponIdPre = 1;
    private int _weaponIdCur = 1;

    
    private void OnEnable()
    {
        ActionManager.OnUpdateInformationWeaponTab += SetWeaponMeshRenderer;
    }
    
    private void OnDisable()
    {
        ActionManager.OnUpdateInformationWeaponTab -= SetWeaponMeshRenderer;
    }
    
    private void Start()
    {
        if (PlayerPrefs.HasKey("WeaponId"))
        {
            weaponList[DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId)-1].SetActive(true);
            _weaponIdPre = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId);
            _weaponIdCur = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId);
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
        if (DataManager.Instance.WeaponsDatas[_weaponIdCur].Rest.Item1 == 1)
        {
            DataManager.Instance.SaveDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId,_weaponIdCur);
        }
    }
}
