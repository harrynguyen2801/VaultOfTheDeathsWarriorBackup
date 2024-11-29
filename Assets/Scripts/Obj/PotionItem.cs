using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionItem : MonoBehaviour
{
    public Image itemBg;
    public Image imgName;

    public Image potionImg;
    public Image potionImgChoose;
    public Button btnBuy;
    public GameObject bgNotBuy;
    public Button choosePotion;
    public TextMeshProUGUI potionPrice;
    public TextMeshProUGUI potionName;
    public GameObject quantity;
    public TextMeshProUGUI tmpQuantity;
    public int potionId;
    private Tuple<string, string, int, int, int, int, string,Tuple<Tuple<int, int>>> _dataPotion;
    private bool _isInventory;

    public void SetDataPotion(int potionIdx, Tuple<string, string, int, int, int, int, string,Tuple<Tuple<int, int>>> data,bool isInventory)
    {
        _isInventory = isInventory;
        _dataPotion = data;
        potionImg.sprite = Resources.Load<Sprite>("Potion/" + potionIdx);
        potionName.text = data.Item1;
        potionId = potionIdx;
        if (isInventory)
        {
            bgNotBuy.SetActive(false);
            quantity.SetActive(true);
            tmpQuantity.text = DataManager.Instance.DataPotionPlayerBuy[potionId].Item2.ToString();
        }
        else
        {
            bgNotBuy.SetActive(true);
            potionPrice.text = data.Rest.Item1.Item1.ToString();
        }
        choosePotion.onClick.AddListener(ChoosePotion);
    }

    public void ShowItem(float time)
    {
        StartCoroutine(Show(time));
    }

    IEnumerator Show(float time)
    {
        yield return new WaitForSeconds(time);
        potionImg.DOFade(1f, 0.2f);
        potionName.DOFade(1f, 0.2f);
        potionPrice.DOFade(1f, 0.2f);
        itemBg.DOFade(.1f, 0.2f);
        imgName.DOFade(1f, 0.2f);
        potionImgChoose.DOFade(1f, 0.2f);
    }

    public void ChoosePotion()
    {
        NavContentPotion navContentPotion = GetComponentInParent<NavContentPotion>();
        for (int i = 0; i < navContentPotion.listPotionItem.Count; i++)
        {
            navContentPotion.listPotionItem[i].GetComponent<PotionItem>().potionImgChoose.gameObject.SetActive(false);
        }
        potionImgChoose.gameObject.SetActive(true);
        ActionManager.OnUpdateInformationPotionTab?.Invoke(potionId);
        if (_isInventory)
        {
            var check = 0;
            if (PotionEquipPlayer.Instance.currentEquipChoose == (int)DataManager.Epotion.Potion1)
            {
                check = DataManager.Instance.GetUserPotion(DataManager.Epotion.Potion2);
            }
            else
            {
                check = DataManager.Instance.GetUserPotion(DataManager.Epotion.Potion1);
            }
            Debug.Log("ID equip potion " + check);
            if (check == potionId)
            {
                Debug.Log("Equip Potion Is Ready, Please equip other potion");
            }
            else
            {
                ActionManager.OnUpdatePotionChoiceEquip?.Invoke(potionId);
            }
        }
    }

    public void BuyWeapon()
    {
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin) < _dataPotion.Rest.Item1.Item1)
        {
            //TODO active anoucement
            ActionManager.OnUpdateAnoucement?.Invoke("You haven't enough coin for this potion.");
        }
        else
        {
            var coin = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin);
            coin -= _dataPotion.Rest.Item1.Item1;
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.Coin,coin);
            ActionManager.OnUpdateCoin?.Invoke();
            var currentCount = DataManager.Instance.DataPotionPlayerBuy.Single(data => data.Key == potionId).Value.Item2;
            // var currentCount = DataManager.Instance.DataPotionPlayerBuy[potionId].Item2;
            var newData = Tuple.Create(potionId, currentCount + 1);
            DataManager.Instance.DataPotionPlayerBuy[potionId] = newData;
            DataManager.Instance.SaveDataPotion();
            ChoosePotion();
        }
    }
}
