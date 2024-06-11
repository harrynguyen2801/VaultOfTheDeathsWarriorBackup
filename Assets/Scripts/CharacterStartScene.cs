using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStartScene : MonoBehaviour
{
    public GameObject[] weaponList;
    private int weaponIdPre = 1;
    private int weaponIdCur = 1;
    
    public void SetWeaponMeshRenderer(int weaponId)
    {
        weaponIdPre = weaponIdCur;
        weaponIdCur = weaponId;
        weaponList[weaponIdPre-1].SetActive(false);
        weaponList[weaponIdCur-1].SetActive(true);
        PlayerPrefs.SetInt("WeaponId",weaponIdCur);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("WeaponId"));
    }
}
