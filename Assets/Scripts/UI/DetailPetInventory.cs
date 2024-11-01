using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailPetInventory : MonoBehaviour
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

    private void OnEnable()
    {
        ActionManager.OnUpdateInformationPetTab += SetDataPetWithLevel;
    }

    private void OnDisable()
    {
        ActionManager.OnUpdateInformationPetTab -= SetDataPetWithLevel;
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
        DataManager.Instance.SaveDataPrefPlayer(DataManager.EDataPlayerEquip.PetId,idx);
    }
}
