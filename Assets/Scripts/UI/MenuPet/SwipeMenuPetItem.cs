using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenuPetItem : MonoBehaviour
{
    public Image imgPet;
    public Button btnDetail;
    public TextMeshProUGUI tmpName;
    public TextMeshProUGUI tmpBuy;
    public GameObject tmpOwned;
    public GameObject btnBuy;

    [SerializeField]
    private EnumManager.EPet _ePet;

    public void SetupItem(EnumManager.EPet EPet)
    {
        _ePet = EPet;
        imgPet.sprite = Resources.Load<Sprite>("Pet/" + (int)_ePet);
        tmpName.text = _ePet.ToString();
        if (DataManager.Instance.PetData.Single(x => x.Key == (int)_ePet).Value.Item7 == 0)
        {
            btnBuy.SetActive(true);
            tmpBuy.text = DataManager.Instance.PetData.Single(x => x.Key == (int)_ePet).Value.Rest.Item1.ToString();
            tmpOwned.SetActive(false);
        }
        else
        {
            btnBuy.SetActive(false);
            tmpOwned.SetActive(true);
        }
    }
    
    public void SetupItem()
    {
        imgPet.sprite = Resources.Load<Sprite>("Pet/" + (int)_ePet);
        tmpName.text = _ePet.ToString();
        if (DataManager.Instance.PetData.Single(x => x.Key == (int)_ePet).Value.Item7 == 0)
        {
            btnBuy.SetActive(true);
            tmpBuy.text = DataManager.Instance.PetData.Single(x => x.Key == (int)_ePet).Value.Rest.Item1.ToString();
            tmpOwned.SetActive(false);
        }
        else
        {
            btnBuy.SetActive(false);
            tmpOwned.SetActive(true);
        }
    }
    
    public void SetBtnDetail()
    {
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Tutorial) == 0)
        {
            PetMenuManager.Instance.tutorialPetScreen.HideTutorialBtn();
        }
        Debug.Log("Detail | " + _ePet);
        //TODO active screen detail
        PetMenuManager.Instance.ShowPetDetailPanel((int)_ePet);
    }
}
