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
        this.RegisterListener(EventID.OnSkillGuardActivate,(param) => OnSkillActivate());
        isCD = false;
        idSkill = DataManager.Instance.GetUserSkill(eskill);
        skillImg.sprite = Resources.Load<Sprite>("Skills/" + eskill + "/" + idSkill);
        timeSkillCD = DataManager.Instance.GetSkillDataByID(idSkill, eskill).Item4;
        Debug.Log("CD time: " + timeSkillCD);
    }

    private void OnSkillActivate()
    {
        Debug.Log("CD activate");
        isCD = true;
    }

    private void Update()
    {
        if (isCD)
        {
            blackBgCD.gameObject.SetActive(true);
            blackBgCD.fillAmount -= 1f / timeSkillCD * Time.deltaTime;
            Debug.Log("CD Imgfill : " + blackBgCD.fillAmount);
            if (blackBgCD.fillAmount <= 0)
            {
                blackBgCD.fillAmount = 1f;
                blackBgCD.gameObject.SetActive(false);
                isCD = false;
            }
        }
    }
}
