using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetDetailManager : MonoBehaviour
{
    [Header("Detail")] 
    public TextMeshProUGUI tmpDetail;
    public TextMeshProUGUI tmpName;
    public TextMeshProUGUI tmpPower;
    public TextMeshProUGUI tmpLevel;
    public Image petElement;

    [Header("Stats")] 
    public TextMeshProUGUI tmpHp;
    public TextMeshProUGUI tmpAtk;
    public TextMeshProUGUI tmpDef;
    
    [Header("Buy")] 
    public TextMeshProUGUI tmpCost;

    public Button btnBuy;

    public List<PetAvatarLevelItem> listPetAvaItem = new List<PetAvatarLevelItem>();
    
    public ParticleSystem vfxAppear;

    public void SetDataPetLevel(EnumManager.EPet ePet)
    {
        for (int i = 0; i < listPetAvaItem.Count; i++)
        {
            listPetAvaItem[i].SetDataLevelPet(ePet, i+1);
        }
    }

    private Tuple<string, int, int, int, int, string, int, Tuple<int>> _petData;
    public void SetDataPet(int idx)
    {
        _petData = DataManager.Instance.PetData.ElementAt(idx - 1).Value;
        tmpDetail.text = DataManager.Instance.PetData.ElementAt(idx-1).Value.Item6;
        tmpHp.text = DataManager.Instance.PetData.ElementAt(idx-1).Value.Item3.ToString();
        tmpAtk.text = DataManager.Instance.PetData.ElementAt(idx-1).Value.Item4.ToString();
        tmpDef.text = DataManager.Instance.PetData.ElementAt(idx-1).Value.Item5.ToString();
        tmpName.text = DataManager.Instance.PetData.ElementAt(idx-1).Value.Item1;
        
        int power = DataManager.Instance.PetData.ElementAt(idx-1).Value.Item3 +
                    DataManager.Instance.PetData.ElementAt(idx-1).Value.Item4 + 
                    DataManager.Instance.PetData.ElementAt(idx-1).Value.Item5;
        
        tmpPower.text = power.ToString();
        petElement.sprite = Resources.Load<Sprite>("Element/" + (EnumManager.EElement)Enum.ToObject(typeof(EnumManager.EElement),idx));
        tmpCost.text = DataManager.Instance.PetData.ElementAt(idx-1).Value.Rest.Item1.ToString();
        tmpLevel.text = "LevelOpen 1";
        vfxAppear.Play();
        
        if (DataManager.Instance.PetData.ElementAt(idx-1).Value.Item7 == 1)
        {
            btnBuy.gameObject.SetActive(false);
        }
        else
        {
            btnBuy.gameObject.SetActive(true);
            btnBuy.onClick.RemoveAllListeners();
            btnBuy.onClick.AddListener(() => BuyPet(idx));
        }
    }
    
    public void SetDataPetWithLevel(int idx, int level)
    {
        tmpDetail.text = DataManager.Instance.PetData.ElementAt(idx-1).Value.Item6;
        tmpHp.text = (DataManager.Instance.PetData.ElementAt(idx-1).Value.Item3 * level).ToString();
        tmpAtk.text = (DataManager.Instance.PetData.ElementAt(idx-1).Value.Item4 * level).ToString();
        tmpDef.text = (DataManager.Instance.PetData.ElementAt(idx-1).Value.Item5 * level).ToString();
        tmpName.text = DataManager.Instance.PetData.ElementAt(idx-1).Value.Item1;
        
        int power = (DataManager.Instance.PetData.ElementAt(idx-1).Value.Item3 +
                    DataManager.Instance.PetData.ElementAt(idx-1).Value.Item4 + 
                    DataManager.Instance.PetData.ElementAt(idx-1).Value.Item5) * level;
        
        tmpPower.text = power.ToString();
        petElement.sprite = Resources.Load<Sprite>("Element/" + (EnumManager.EElement)Enum.ToObject(typeof(EnumManager.EElement),idx));
        tmpLevel.text = "LevelOpen " + level;
        vfxAppear.Play();
    }

    public void BuyPet(int idx)
    {
        Debug.Log("buy pet coin + " + _petData.Rest.Item1 + " coin have:  " + DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin));

        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin) < _petData.Rest.Item1)
        {
            ActionManager.OnUpdateAnoucement?.Invoke("You do not have enough coins for this pet.");
        }
        else
        {
            Debug.Log("buy pet + " + (EnumManager.EPet)Enum.ToObject(typeof(EnumManager.EPet),idx));
            var coin = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin);
            coin -= _petData.Rest.Item1;
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.Coin,coin);
            int val = 1;
            Tuple<string, int, int, int, int, string, int,Tuple<int>> petDataNew = 
                new Tuple<string, int, int, int, int, string, int, Tuple<int>>
                (_petData.Item1, _petData.Item2, _petData.Item3, _petData.Item4,
                    _petData.Item5,_petData.Item6, val ,_petData.Rest);
            DataManager.Instance.PetData[idx] = petDataNew;
            DataManager.Instance.SaveDataPet();
            DataManager.Instance.LoadDictDataPet();
            ActionManager.OnUpdateCoin?.Invoke();
            btnBuy.gameObject.SetActive(false);
        }
    }
}
