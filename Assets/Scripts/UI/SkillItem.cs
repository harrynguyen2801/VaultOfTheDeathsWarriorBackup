using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour
{
    public TextMeshProUGUI nameSkill;
    public TextMeshProUGUI descSkill;
    public Image imgSkill;
    public Button chooseSkill;

    public Button lockSkill;

    private int _idSkill;
    private Tuple<string, int,int, int, int, string,int> dataSkill;
    public void SetDataSkill(int idSkill, Tuple<string,int,int,int,int,string,int> data, string skillType)
    {
        dataSkill = data;
        imgSkill.sprite = Resources.Load<Sprite>("Skills/" + skillType + "/" + idSkill);
        _idSkill = idSkill;
        nameSkill.text = data.Item1;
        descSkill.text = data.Item6;
        if (dataSkill.Item7 <= DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.Level))
        {
            chooseSkill.onClick.AddListener(ChooseSkill);
        }
        else
        {
            lockSkill.gameObject.SetActive(true);
            lockSkill.onClick.AddListener(LockSkill);
            Debug.Log("Lock Skill Item " + dataSkill.Item1);
        }
    }
    
    public void ChooseSkill()
    {
        SkillsScreenManager.Instance.btnSelected.GetComponent<CircleSkill>().skillImg.sprite = imgSkill.sprite;
        SkillsScreenManager.Instance.InformationSkills.gameObject.SetActive(true);
        SkillsScreenManager.Instance.InformationSkills.SetInformationSkills(dataSkill);
        DataManager.Instance.SaveUserSkill(SkillsScreenManager.Instance.btnSelected.GetComponent<CircleSkill>().eSkills,_idSkill);
    }

    public void LockSkill()
    {
        GetComponentInParent<InventoryPanel>().anoucement.ActiveAnoucement("Please Level Up To Level " + dataSkill.Item7 + " To Unlock Skill");
    }
}
