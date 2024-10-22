using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsScreenManager : MonoBehaviour
{
    public GameObject skillPanel;
    public GameObject arrowDecor;
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
        ChangeRotationArrow(90);
        skillPanel.GetComponent<NavContentSkills>().ShowGuardSkillsList();
        // btnSelected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        btnSelected = skillCircleList[0].gameObject;
    }
    
    public void OpenSwordSkillPanel()
    {
        ChangeRotationArrow(0);
        skillPanel.GetComponent<NavContentSkills>().ShowSwordSkillsList();
        // btnSelected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        btnSelected = skillCircleList[2].gameObject;
    }
    
    public void OpenMagicSkillPanel()
    {
        ChangeRotationArrow(45);
        skillPanel.GetComponent<NavContentSkills>().ShowMagicSkillsList();
        // btnSelected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        btnSelected = skillCircleList[1].gameObject;
    }

    private void ChangeRotationArrow(int deg)
    {
        StartCoroutine(Rotation(deg));
    }

    IEnumerator Rotation(int deg)
    {
        Debug.Log(arrowDecor.transform.localRotation.eulerAngles.z);
        float t = 0f;
        while (t < 0.5f)
        {
            float zDegree = Mathf.Lerp(arrowDecor.transform.localRotation.eulerAngles.z, deg, t / 0.4f);
            arrowDecor.transform.eulerAngles = new Vector3(0,0, zDegree);
            t += Time.deltaTime;
            yield return null;
        }
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

    private void OnDisable()
    {
        arrowDecor.transform.Rotate(new Vector3(arrowDecor.transform.rotation.x, arrowDecor.transform.rotation.y, 0));
    }
}
