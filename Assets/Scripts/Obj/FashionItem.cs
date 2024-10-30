using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FashionItem : MonoBehaviour
{
    public Image itemBg;
    public Image imgName;

    public Image fashionImg;
    public Image fashionImgChoose;
    public Button btnBuy;
    public GameObject bgNotBuy;
    public Button chooseFashion;
    public TextMeshProUGUI fashionPrice;
    public TextMeshProUGUI fashionName;
    public int FashionId;
    private Tuple<int, int> _dataFashion;
    private EnumManager.EFashionType _eFashionType;
    public void SetDataFashion(int fashionId, Tuple<int,int> dataFashionParam, EnumManager.EFashionType type)
    {
        _dataFashion = dataFashionParam;
        fashionImg.sprite = Resources.Load<Sprite>("Fashion/" + "Female/" + type + "/" + fashionId);
        _eFashionType = type;
        FashionId = fashionId;
        if (_dataFashion.Item2 == 1)
        {
            bgNotBuy.SetActive(false);
        }
        else
        {
            bgNotBuy.SetActive(true);
            fashionPrice.text = _dataFashion.Item1.ToString();
        }
        chooseFashion.onClick.AddListener(ChooseFashion);
    }

    public void ShowItem(float time)
    {
        StartCoroutine(Show(time));
    }

    IEnumerator Show(float time)
    {
        yield return new WaitForSeconds(time);
        fashionImg.DOFade(1f, 0.2f);
        fashionName.DOFade(1f, 0.2f);
        fashionPrice.DOFade(1f, 0.2f);
        itemBg.DOFade(.1f, 0.2f);
        imgName.DOFade(1f, 0.2f);
        fashionImgChoose.DOFade(1f, 0.2f);
    }

    public void ChooseFashion()
    {
        NavContentFashion navContentFashion = GetComponentInParent<NavContentFashion>();
        for (int i = 0; i < navContentFashion.listFashionItem.Count; i++)
        {
            navContentFashion.listFashionItem[i].GetComponent<FashionItem>().fashionImgChoose.gameObject.SetActive(false);
        }
        fashionImgChoose.gameObject.SetActive(true);
        
        //test set fashion
        for (int i = 0; i < CharacterCreatorScreen.Instance.setCharacter.itemGroups[(int)_eFashionType-1].items.Length; i++)
        {
            CharacterCreatorScreen.Instance.DeactiveFashionItem(CharacterCreatorScreen.Instance.setCharacter.itemGroups[(int)_eFashionType-1], i);
        }
        
        CharacterCreatorScreen.Instance.ActiveFashionItem(CharacterCreatorScreen.Instance.setCharacter.itemGroups[(int)_eFashionType-1], FashionId);
    }

    public void BuyFashion()
    {
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin) < _dataFashion.Item1)
        {
            //TODO active anoucement
            ActionManager.OnUpdateAnoucement?.Invoke("You haven't enough coin for this " + _eFashionType);
        }
        else
        {
            var coin = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin);
            coin -= _dataFashion.Item2;
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.Coin,coin);
            ActionManager.OnUpdateCoin?.Invoke();
            Tuple<int, int> dataFashionNew =
                new Tuple<int, int>(_dataFashion.Item1,1);
            // DataManager.Instance.WeaponsDatas[FashionId] = dataWeaponNew;
            DataManager.Instance.SaveDataWeapon();
            var data =  DataManager.Instance.GetDictDataFashionWithType(_eFashionType);
            data[FashionId] = dataFashionNew;
            DataManager.Instance.SaveDataFashionWithType(_eFashionType);
            bgNotBuy.SetActive(false);
            ChooseFashion();
            gameObject.SetActive(false);
        }
    }
}
