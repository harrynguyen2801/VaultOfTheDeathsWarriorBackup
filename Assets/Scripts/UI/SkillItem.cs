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

    private int _idSkill;
    private Tuple<string, int, int, int, string> dataSkill;
    public void SetDataSkill(int idSkill, Tuple<string,int,int,int,string> data, string skillType)
    {
        dataSkill = data;
        imgSkill.sprite = Resources.Load<Sprite>("Skills/" + skillType + "/" + idSkill);
        _idSkill = idSkill;
        nameSkill.text = data.Item1;
        descSkill.text = data.Item5;
        chooseSkill.onClick.AddListener(ChooseSkill);
    }
    
    public void ChooseSkill()
    {
        SkillsScreenManager.Instance.btnSelected.GetComponent<CircleSkill>().skillImg.sprite = imgSkill.sprite;
        SkillsScreenManager.Instance.InformationSkills.gameObject.SetActive(true);
        SkillsScreenManager.Instance.InformationSkills.SetInformationSkills(dataSkill);
        DataManager.Instance.SaveUserSkill(SkillsScreenManager.Instance.btnSelected.GetComponent<CircleSkill>().eSkills,_idSkill);
    }
}
