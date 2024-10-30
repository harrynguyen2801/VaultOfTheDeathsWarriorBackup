using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NavContentSkills : MonoBehaviour
{
    public GameObject content;

    public GameObject pbSkill;

    public Image skillPanelTop;
    public Image skillPanelBot;

    public List<GameObject> listSkillsItem = new List<GameObject>();

    public void CloseSkillPanel()
    {
        skillPanelBot.transform.localPosition = new Vector3(0f,347f,0f);
        skillPanelBot.color = new Color(255f,255f,255f,0f);
        skillPanelTop.color = new Color(255f,255f,255f,0f);
        skillPanelTop.transform.localPosition = new Vector3(0f,-383f,0f);
        listSkillsItem.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }
    
    public void ShowSkillPanelDecor()
    {
        skillPanelBot.DOFade(1f,0.15f);
        skillPanelTop.DOFade(1f,0.15f);
        DOTween.Sequence().SetDelay(0.3f).Append(skillPanelBot.transform.DOMoveY(500f,0.15f));
        DOTween.Sequence().SetDelay(0.3f).Append(skillPanelTop.transform.DOMoveY(400f,0.15f));
    }
    
    public void ShowGuardSkillsList()
    {
        StartCoroutine(ShowGuardSkills());
    }
    
    public void ShowSwordSkillsList()
    {
        StartCoroutine(ShowSwordSkills());

    }
    
    public void ShowMagicSkillsList()
    {
        StartCoroutine(ShowMagicSkills());

    }

    IEnumerator ShowGuardSkills()
    {
        ShowSkillPanelDecor();
        yield return new WaitForSeconds(0.2f);
        InitGuardSkillsList();
    }
    
    IEnumerator ShowSwordSkills()
    {
        ShowSkillPanelDecor();
        yield return new WaitForSeconds(0.2f);
        InitSwordSkillsList();
    }
    
    IEnumerator ShowMagicSkills()
    {
        ShowSkillPanelDecor();
        yield return new WaitForSeconds(0.2f);
        InitMagicSkillsList();
    }

    IEnumerator FadeInSkills()
    {
        foreach (var skillItem in listSkillsItem)
        {
            SkillItem temp = skillItem.GetComponent<SkillItem>();
            temp.imgSkill.DOFade(1f,0.1f);
            temp.nameSkill.DOFade(1f,0.1f);
            temp.descSkill.DOFade(1f,0.1f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void InitGuardSkillsList()
    {
        listSkillsItem.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        foreach (var data in DataManager.Instance.SkillsGuardData)
        {
            if (data.Value.Item7.Item2 == 1)
            {
                var skill = Instantiate(pbSkill,content.transform);
                skill.GetComponent<SkillItem>().SetDataSkill(data.Key,data.Value,"Guard");
                listSkillsItem.Add(skill);
            }
        }

        StartCoroutine(FadeInSkills());
    }
    
    public void InitSwordSkillsList()
    {
        listSkillsItem.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        foreach (var data in DataManager.Instance.SkillsSwordData)
        {
            if (data.Value.Item7.Item2 == 1)
            {
                var skill = Instantiate(pbSkill, content.transform);
                skill.GetComponent<SkillItem>().SetDataSkill(data.Key, data.Value, "Sword");
                listSkillsItem.Add(skill);
            }
        }
        StartCoroutine(FadeInSkills());
    }
    
    public void InitMagicSkillsList()
    {
        listSkillsItem.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        foreach (var data in DataManager.Instance.SkillsMagicData)
        {
            if (data.Value.Item7.Item2 == 1)
            {
                var skill = Instantiate(pbSkill, content.transform);
                skill.GetComponent<SkillItem>().SetDataSkill(data.Key, data.Value, "Magic");
                listSkillsItem.Add(skill);
            }
        }
        StartCoroutine(FadeInSkills());
    }
}
