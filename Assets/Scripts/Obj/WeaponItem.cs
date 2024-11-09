using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItem : MonoBehaviour
{
    public Image itemBg;
    public Image imgName;

    public Image weaponImg;
    public Image weaponImgChoose;
    public Button btnBuy;
    public GameObject bgNotBuy;
    public Button chooseWeapon;
    public TextMeshProUGUI weaponPrice;
    public TextMeshProUGUI weaponName;
    public int WeaponId;
    private Tuple<string, string, int, int, int, string,int, Tuple<int>> dataWeapon;
    private bool _isInventory;

    public void SetDataWeapon(int weaponId, Tuple<string, string, int, int, int, string,int, Tuple<int>> data,bool isInventory)
    {
        _isInventory = isInventory;
        dataWeapon = data;
        weaponImg.sprite = Resources.Load<Sprite>("WeaponSprites/" + weaponId);
        weaponName.text = data.Item1;
        WeaponId = weaponId;
        if (data.Rest.Item1 == 1)
        {
            bgNotBuy.SetActive(false);
        }
        else
        {
            bgNotBuy.SetActive(true);
            weaponPrice.text = data.Item7.ToString();
        }
        chooseWeapon.onClick.AddListener(ChooseWeapon);
    }

    public void ShowItem(float time)
    {
        StartCoroutine(Show(time));
    }

    IEnumerator Show(float time)
    {
        yield return new WaitForSeconds(time);
        weaponImg.DOFade(1f, 0.2f);
        weaponName.DOFade(1f, 0.2f);
        weaponPrice.DOFade(1f, 0.2f);
        itemBg.DOFade(.1f, 0.2f);
        imgName.DOFade(1f, 0.2f);
        weaponImgChoose.DOFade(1f, 0.2f);
    }

    public void ChooseWeapon()
    {
        NavContentWeapon navContentWeapon = GetComponentInParent<NavContentWeapon>();
        for (int i = 0; i < navContentWeapon.listWeaponItem.Count; i++)
        {
            navContentWeapon.listWeaponItem[i].GetComponent<WeaponItem>().weaponImgChoose.gameObject.SetActive(false);
        }
        weaponImgChoose.gameObject.SetActive(true);
        ActionManager.OnUpdateInformationWeaponTab?.Invoke(WeaponId);
        ActionManager.OnUpdateWeaponPlayer?.Invoke(_isInventory,WeaponId);
    }

    public void BuyWeapon()
    {
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialVillage) == 0)
        {
            PropertyHeroScreen.Instance.tutorial.GetComponent<TutorialEquip>().ShowTutorialHori(1);
            PropertyHeroScreen.Instance.tutorial.GetComponent<TutorialEquip>().btnClose.SetActive(false);
            PropertyHeroScreen.Instance.tutorial.GetComponent<TutorialEquip>().arrow1.SetActive(false);
        }
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin) < dataWeapon.Item7)
        {
            //TODO active anoucement
            ActionManager.OnUpdateAnoucement?.Invoke("You haven't enough coin for this weapon.");
        }
        else
        {
            var coin = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin);
            coin -= dataWeapon.Item7;
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.Coin,coin);
            ActionManager.OnUpdateCoin?.Invoke();
            Tuple<string, string, int, int, int, string,int, Tuple<int>> dataWeaponNew =
                new Tuple<string, string, int, int, int, string,int, Tuple<int>>(dataWeapon.Item1,dataWeapon.Item2,dataWeapon.Item3,dataWeapon.Item4,dataWeapon.Item5,dataWeapon.Item6,dataWeapon.Item7,new Tuple<int>(1));
            DataManager.Instance.WeaponsDatas[WeaponId] = dataWeaponNew;
            DataManager.Instance.SaveDataWeapon();
            bgNotBuy.SetActive(false);
            ChooseWeapon();
            gameObject.SetActive(false);
        }
    }
}
