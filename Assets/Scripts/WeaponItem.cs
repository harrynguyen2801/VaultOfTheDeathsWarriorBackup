using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItem : MonoBehaviour
{
    public Image weaponImg;
    public Image weaponImgChoose;
    public TextMeshProUGUI weaponName;
    public int WeaponId;
    private Tuple<string, string, int, int, int, int, string> dataWeapon;
    public void SetDataWeapon(int weaponId, Tuple<string,string,int,int,int,int,string> data)
    {
        dataWeapon = data;
        weaponImg.sprite = Resources.Load<Sprite>("WeaponSprites/" + weaponId);
        weaponName.text = data.Item1;
        WeaponId = weaponId;

        if (weaponId == DataManager.Instance.LoadDataInt(DataManager.dataName.WeaponId))
        {
            weaponImgChoose.gameObject.SetActive(true);
            PropertyHeroScreen.Instance.InfomationTab.gameObject.SetActive(true);
            PropertyHeroScreen.Instance.InfomationTab.SetInformationWeapon(dataWeapon);
        }
    }

    public void ChooseWeapon()
    {
        PropertyHeroScreen.Instance.characterStartScene.SetWeaponMeshRenderer(WeaponId);
        NavContent navContent = GetComponentInParent<NavContent>();
        for (int i = 0; i < navContent.listWeaponItem.Count; i++)
        {
            navContent.listWeaponItem[i].GetComponent<WeaponItem>().weaponImgChoose.gameObject.SetActive(false);
        }
        weaponImgChoose.gameObject.SetActive(true);
        
        PropertyHeroScreen.Instance.InfomationTab.gameObject.SetActive(true);
        PropertyHeroScreen.Instance.InfomationTab.SetInformationWeapon(dataWeapon);
    }
}
