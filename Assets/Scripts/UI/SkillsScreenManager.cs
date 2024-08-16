using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsScreenManager : MonoBehaviour
{
    public GameObject skillPanel;
    public Button[] skillCircleList;

    public GameObject btnSelected;
    public InformationSkills InformationSkills;

    public static SkillsScreenManager Instance => _instance;
    private static SkillsScreenManager _instance;
    private DataManager.ESkills eskills;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < skillCircleList.Length; i++)
        {
            AddBtnOnClick(skillCircleList[i]);
        }
    }

    public void OpenGuardSkillPanel()
    {
        skillPanel.GetComponent<NavContentSkills>().ShowGuardSkillsList();
        btnSelected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
    }
    
    public void OpenSwordSkillPanel()
    {
        skillPanel.GetComponent<NavContentSkills>().ShowSwordSkillsList();
        btnSelected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
    }
    
    public void OpenMagicSkillPanel()
    {
        skillPanel.GetComponent<NavContentSkills>().ShowMagicSkillsList();
        btnSelected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
    }

    public void AddBtnOnClick(Button ck)
    {
        eskills = ck.gameObject.GetComponent<CircleSkill>().eSkills;
        switch (eskills)
        {
            case DataManager.ESkills.Guard:
                ck.onClick.AddListener(OpenGuardSkillPanel);
                break;
            case DataManager.ESkills.Sword:
                ck.onClick.AddListener(OpenSwordSkillPanel);
                break;
            case DataManager.ESkills.Magic:
                ck.onClick.AddListener(OpenMagicSkillPanel);
                break;
        }
        ck.onClick.AddListener(ActiveInformationTabSkillDefault);
    }

    public void ActiveInformationTabSkillDefault()
    {
        eskills = btnSelected.GetComponent<CircleSkill>().eSkills;
        InformationSkills.gameObject.SetActive(true);
        InformationSkills.SetInformationSkills(DataManager.Instance.GetSkillDataByID(DataManager.Instance.GetUserSkill(eskills),eskills));
    }
}
