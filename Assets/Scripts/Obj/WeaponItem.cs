using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItem : MonoBehaviour
{
    public Image weaponImg;
    public Image weaponImgChoose;
    public Button btnBuy;
    public GameObject bgNotBuy;
    public Button chooseWeapon;
    public TextMeshProUGUI weaponPrice;
    public TextMeshProUGUI weaponName;
    public int WeaponId;
    private Tuple<string, string, int, int, int, string,int, Tuple<int>> dataWeapon;

    private PropertyHeroScreen _propertyHeroScreen;
    public void SetDataWeapon(int weaponId, Tuple<string, string, int, int, int, string,int, Tuple<int>> data)
    {
        dataWeapon = data;
        weaponImg.sprite = Resources.Load<Sprite>("WeaponSprites/" + weaponId);
        weaponName.text = data.Item1;
        WeaponId = weaponId;
        chooseWeapon.onClick.AddListener(ChooseWeapon);
        bgNotBuy.SetActive(false);
        if (weaponId == DataManager.Instance.LoadDataInt(DataManager.dataName.WeaponId))
        {
            weaponImgChoose.gameObject.SetActive(true);
            PropertyHeroScreen.Instance.InfomationTab.gameObject.SetActive(true);
            PropertyHeroScreen.Instance.InfomationTab.SetInformationWeapon(dataWeapon);
        }
    }

    public void SetDataWeaponInventory(int weaponId, Tuple<string, string, int, int, int, string,int, Tuple<int>> data)
    {
        dataWeapon = data;
        weaponImg.sprite = Resources.Load<Sprite>("WeaponSprites/" + weaponId);
        weaponName.text = data.Item1;
        WeaponId = weaponId;
        bgNotBuy.SetActive(true);
        weaponPrice.text = data.Item7.ToString();
        chooseWeapon.onClick.AddListener(ChooseWeaponInventory);
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
    
    public void ChooseWeaponInventory()
    {
        PropertyHeroScreen.Instance.characterStartScene.SetWeaponMeshRendererInventory(WeaponId);
        NavContent navContent = GetComponentInParent<NavContent>();
        for (int i = 0; i < navContent.listWeaponItem.Count; i++)
        {
            navContent.listWeaponItem[i].GetComponent<WeaponItem>().weaponImgChoose.gameObject.SetActive(false);
        }
        weaponImgChoose.gameObject.SetActive(true);
        
        PropertyHeroScreen.Instance.InfomationTab.gameObject.SetActive(true);
        PropertyHeroScreen.Instance.InfomationTab.SetInformationWeapon(dataWeapon);
    }

    public void BuyWeapon()
    {
        _propertyHeroScreen = GetComponentInParent<PropertyHeroScreen>();
        if (DataManager.Instance.LoadDataInt(DataManager.dataName.Coin) < dataWeapon.Item7)
        {
            _propertyHeroScreen.anoucement.gameObject.SetActive(true);
            _propertyHeroScreen.anoucement.ActiveAnoucement();
        }
        else
        {
            Tuple<string, string, int, int, int, string,int, Tuple<int>> dataWeaponNew =
                new Tuple<string, string, int, int, int, string,int, Tuple<int>>(dataWeapon.Item1,dataWeapon.Item2,dataWeapon.Item3,dataWeapon.Item4,dataWeapon.Item5,dataWeapon.Item6,dataWeapon.Item7,new Tuple<int>(1));
            DataManager.Instance.weaponsData[WeaponId] = dataWeaponNew;
            DataManager.Instance.SaveDataWeapon();
            bgNotBuy.SetActive(false);
            ChooseWeapon();
            gameObject.SetActive(false);
        }
    }
}
