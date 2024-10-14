using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenuPetItem : MonoBehaviour
{
    public Image imgPet;
    public Button btnBuy;
    public Button btnDetail;
    public TextMeshProUGUI tmpName;
    [SerializeField]
    private EnumManager.EPet _ePet;

    private Dictionary<int, Tuple<string, int, int, int, int, string, int,Tuple<int>>> _petData;
    public void SetupItem(EnumManager.EPet EPet)
    {
        _ePet = EPet;
        imgPet.sprite = Resources.Load<Sprite>("Pet/" + (int)_ePet);
        tmpName.text = _ePet.ToString();
        _petData = DataManager.Instance.PetData;
    }

    public void Buy()
    {
        Debug.Log("Buy | " + _ePet);
        //TODO Buy action
        
    }
    public void SetBtnDetail()
    {
        Debug.Log("Detail | " + _ePet);
        //TODO active screen detail
        PetMenuManager.Instance.ShowPetDetailPanel((int)_ePet);
    }
}
