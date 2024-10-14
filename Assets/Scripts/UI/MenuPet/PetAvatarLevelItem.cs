using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetAvatarLevelItem : MonoBehaviour
{
    public Image imgAva;
    public Image imgChoice;
    public Button btnChoice;
    public void SetDataLevelPet(EnumManager.EPet ePet, int level)
    {
        imgAva.sprite = Resources.Load<Sprite>("PetAvatar/" + ePet +"/" + level);
        SetBtnChoice(ePet,level);
    }

    public void SetBtnChoice(EnumManager.EPet ePet, int level)
    {
        imgChoice.gameObject.SetActive(true);
        btnChoice.onClick.AddListener(()=>ActivePetWithLevel(ePet,level));
    }

    public void ActivePetWithLevel(EnumManager.EPet ePet, int level)
    {
        PetModelContainer.Instance.listPetModels[PetMenuManager.Instance.petIndexCurrentActive].SetActive(false);
        int newIndex = ((int)ePet - 1) * 3 + level - 1;
        PetMenuManager.Instance.petIndexCurrentActive = newIndex;
        PetModelContainer.Instance.listPetModels[newIndex].SetActive(true);
        PetMenuManager.Instance.petDetailManager.SetDataPetWithLevel((int)ePet, level);
    }
}
