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

    public void SetDataPet(int idx)
    {
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
        tmpLevel.text = "Level 1";
        btnBuy.onClick.AddListener(() => BuyPet(idx));
        vfxAppear.Play();
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
        tmpLevel.text = "Level " + level;
        vfxAppear.Play();
    }

    public void BuyPet(int idx)
    {
        Debug.Log("buy pet + " + (EnumManager.EPet)Enum.ToObject(typeof(EnumManager.EPet),idx));
    }
}
