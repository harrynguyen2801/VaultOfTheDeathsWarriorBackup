using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionEquipPlayer : MonoBehaviour
{
    public PotionEquipInventory euipButton1;
    public PotionEquipInventory euipButton2;
    public int currentEquipChoose;

    public static PotionEquipPlayer Instance => _instance;
    private static PotionEquipPlayer _instance;

    private void OnEnable()
    {
        ActionManager.OnUpdatePotionChoiceEquip += OnSetEquipPotion;
    }

    private void OnDisable()
    {
        ActionManager.OnUpdatePotionChoiceEquip -= OnSetEquipPotion;
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialVillage) == 0)
        {
            euipButton1.SetPotionEquip(1);
            euipButton2.SetPotionEquip(2);
        }
    }

    public void OnSetEquipPotion(int potionId)
    {
        if (currentEquipChoose == 1)
        {
            euipButton1.SetPotionEquip(potionId);
        }
        else
        {
            euipButton2.SetPotionEquip(potionId);
        }
    }
}
