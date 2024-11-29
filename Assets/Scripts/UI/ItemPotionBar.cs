using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Observer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemPotionBar : MonoBehaviour
{
    public int timeSkillCD;
    public Image blackBgCD;
    public Image skillImg;
    public TextMeshProUGUI textTimeCD;
    public DataManager.Epotion epotion;
    public int idSkill;
    private bool isCD;
    public int countItem;
    public int health;
    public int mana;
    public int def;
    private void Start()
    {
        isCD = false;
        idSkill = DataManager.Instance.GetUserPotion(epotion);
        skillImg.sprite = Resources.Load<Sprite>("Potion/" + idSkill);
        timeSkillCD = DataManager.Instance.GetPotionDataByID(idSkill, epotion).Rest.Item1.Item2;
        countItem = DataManager.Instance.DataPotionPlayerBuy.Single(x => x.Key == idSkill).Value.Item2;
        health = DataManager.Instance.PotionsDataDefault.Single(x => x.Key == idSkill).Value.Item3;
        mana = DataManager.Instance.PotionsDataDefault.Single(x => x.Key == idSkill).Value.Item4;
        def = DataManager.Instance.PotionsDataDefault.Single(x => x.Key == idSkill).Value.Item5;

    }

    public void OnPotionActivate()
    {
        countItem--;
        isCD = true;
    }

    private void Update()
    {
        if (isCD)
        {
            blackBgCD.gameObject.SetActive(true);
            blackBgCD.fillAmount -= 1f / timeSkillCD * Time.deltaTime;
            if (blackBgCD.fillAmount <= 0)
            {
                blackBgCD.fillAmount = 1f;
                blackBgCD.gameObject.SetActive(false);
                isCD = false;
                switch (epotion)
                {
                    case DataManager.Epotion.Potion1:
                        this.PostEvent(EventID.OnPotion1CdFinish);
                        break;
                    case DataManager.Epotion.Potion2:
                        this.PostEvent(EventID.OnPotion2CdFinish);
                        break;
                }
            }
        }
    }
}
