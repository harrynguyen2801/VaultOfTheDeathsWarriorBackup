using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePetPanelManager : MonoBehaviour
{
    [Header("Pet 1 elements")] 
    public TextMeshProUGUI tmpHp1;
    public TextMeshProUGUI tmpAtk1;
    public TextMeshProUGUI tmpDef1;
    public TextMeshProUGUI tmpPower1;
    public TextMeshProUGUI tmpLevel1;
    public Image petImg1;
    
    [Header("Pet 2 elements")] 
    public TextMeshProUGUI tmpHp2;
    public TextMeshProUGUI tmpAtk2;
    public TextMeshProUGUI tmpDef2;
    public TextMeshProUGUI tmpPower2;
    public TextMeshProUGUI tmpLevel2;
    public Image petImg2;

    public TextMeshProUGUI tmpCoin;
    private int _coinUpdate;
    private Dictionary<int, Tuple<string, int, int, int, int, string, int, Tuple<int>>> _petData;

    private void OnEnable()
    {
        ShowPetUpgradePanel();
    }

    public void ShowPetUpgradePanel()
    {
        DataManager.Instance.LoadDictDataPet();
        SetDataPet1();
        SetDataPet2();
    }
    public void SetDataPet1()
    {
        _petData = DataManager.Instance.PetData;
        var petIdx = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PetId);
        var level = _petData.ElementAt(petIdx - 1).Value.Item2;
        tmpHp1.text = (_petData.ElementAt(petIdx - 1).Value.Item3 * level).ToString();
        tmpAtk1.text = (_petData.ElementAt(petIdx - 1).Value.Item4 * level).ToString();
        tmpDef1.text = (_petData.ElementAt(petIdx - 1).Value.Item5 * level).ToString();

        int power = (_petData.ElementAt(petIdx - 1).Value.Item3 +
                     _petData.ElementAt(petIdx - 1).Value.Item4 +
                     _petData.ElementAt(petIdx - 1).Value.Item5) * level;
        tmpPower1.text = power.ToString();
        petImg1.sprite = Resources.Load<Sprite>("PetAvatar/" + (EnumManager.EPet)Enum.ToObject(typeof(EnumManager.EPet),petIdx) +"/" + level);
        tmpLevel1.text = "Level " + level;
    }
    
    public void SetDataPet2()
    {
        _petData = DataManager.Instance.PetData;
        var petIdx = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PetId);
        var level = _petData.ElementAt(petIdx - 1).Value.Item2 + 1;
        tmpHp2.text = (_petData.ElementAt(petIdx - 1).Value.Item3 * level).ToString();
        tmpAtk2.text = (_petData.ElementAt(petIdx - 1).Value.Item4 * level).ToString();
        tmpDef2.text = (_petData.ElementAt(petIdx - 1).Value.Item5 * level).ToString();

        int power = (_petData.ElementAt(petIdx - 1).Value.Item3 +
                     _petData.ElementAt(petIdx - 1).Value.Item4 +
                     _petData.ElementAt(petIdx - 1).Value.Item5) * level;
        tmpPower2.text = power.ToString();
        petImg2.sprite = Resources.Load<Sprite>("PetAvatar/" + (EnumManager.EPet)Enum.ToObject(typeof(EnumManager.EPet),petIdx) +"/" + level);

        tmpLevel2.text = "Level " + level;
        tmpCoin.text = (_petData.ElementAt(petIdx-1).Value.Rest.Item1 * level).ToString();
        _coinUpdate = (_petData.ElementAt(petIdx - 1).Value.Rest.Item1 * level);
    }

    public void BtnUpdatePet()
    {
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin) < _coinUpdate)
        {
            //TODO active anoucement
            ActionManager.OnUpdateAnoucement?.Invoke("You haven't enough coin to update pet.");
        }
        else
        {
            var coin = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin);
            coin -= _coinUpdate;
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.Coin,coin);
            ActionManager.OnUpdateCoin?.Invoke();

            _petData = DataManager.Instance.PetData;
            var petIdx = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PetId);
            var level = _petData.ElementAt(petIdx - 1).Value.Item2 + 1;
            Tuple<string, int, int, int, int, string, int, Tuple<int>> dataOld = _petData.ElementAt(petIdx - 1).Value;
            Tuple<string, int, int, int, int, string, int, Tuple<int>> dataNew =
                new Tuple<string, int, int, int, int, string, int, Tuple<int>>(dataOld.Item1, level, dataOld.Item3,
                    dataOld.Item4, dataOld.Item5, dataOld.Item6, dataOld.Item7,dataOld.Rest);
            DataManager.Instance.PetData[petIdx] = dataNew;
            DataManager.Instance.SaveDataPet();
            DataManager.Instance.LoadDictDataPet();
        }
    }
}   
