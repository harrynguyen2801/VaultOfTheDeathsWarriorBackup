using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionEquipInventory : MonoBehaviour
{
    public DataManager.Epotion potionType;
    public Image potionIcon;
    public Image potionChoice;

    private void Start()
    {
        SetPotionEquip(DataManager.Instance.GetUserPotion(potionType));
    }

    public void SetPotionEquip(int potionID)
    {
        DataManager.Instance.SaveUserPotion(potionType, potionID);
        potionIcon.sprite = Resources.Load<Sprite>("Potion/" + potionID);
    }

    public void ChooseBtn()
    {
        potionChoice.gameObject.SetActive(true);
        PotionEquipPlayer.Instance.currentEquipChoose = (int)potionType;
    }
}
