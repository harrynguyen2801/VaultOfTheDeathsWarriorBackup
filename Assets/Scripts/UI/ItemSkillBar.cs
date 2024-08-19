using System;
using System.Collections;
using System.Collections.Generic;
using Observer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemSkillBar : MonoBehaviour
{
    public int timeSkillCD;
    public Image blackBgCD;
    public Image skillImg;
    public TextMeshProUGUI textTimeCD;
    public DataManager.ESkills eskill;
    private int idSkill;
    private bool isCD;
    private void Start()
    {
        isCD = false;
        idSkill = DataManager.Instance.GetUserSkill(eskill);
        skillImg.sprite = Resources.Load<Sprite>("Skills/" + eskill + "/" + idSkill);
        timeSkillCD = DataManager.Instance.GetSkillDataByID(idSkill, eskill).Item4;
    }

    public void OnSkillActivate()
    {
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
                switch (eskill)
                {
                    case DataManager.ESkills.Guard:
                        this.PostEvent(EventID.OnSkillGuardCdFinish);
                        break;
                    case DataManager.ESkills.Magic:
                        this.PostEvent(EventID.OnSkillMagicCdDFinish);
                        break;
                    case DataManager.ESkills.Sword:
                        this.PostEvent(EventID.OnSkillSwordCdDFinish);
                        break;
                }
            }
        }
    }
}
