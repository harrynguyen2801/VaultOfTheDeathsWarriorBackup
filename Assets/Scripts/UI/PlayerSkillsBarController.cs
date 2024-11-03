using System;
using System.Collections;
using System.Collections.Generic;
using Observer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSkillsBarController : MonoBehaviour
{
    public ItemSkillBar ItemSkillBarGuard;
    public ItemSkillBar ItemSkillBarMagic;
    public ItemSkillBar ItemSkillBarSword;

    public bool finishCDGuard;
    public bool finishCDMagic;
    public bool finishCDSword;
    
    [Header("LEVEL")]
    public Image xpBarImage;
    public Text tmpLevel;

    private void OnEnable()
    {
        ActionManager.OnUpdateXpAndLevelPlayer += UpdateXpAndLevelPlayer;
    }

    private void OnDisable()
    {
        ActionManager.OnUpdateXpAndLevelPlayer -= UpdateXpAndLevelPlayer;
    }

    private void Start()
    {
        int lvCurrent = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.Level);
        tmpLevel.text = lvCurrent.ToString();

        finishCDGuard = finishCDSword = finishCDMagic = true;
        
        //Register observer event
        this.RegisterListener(EventID.OnSkillGuardActivate,(param)=> OnSkillGuardActivate());
        this.RegisterListener(EventID.OnSkillMagicActivate,(param)=> OnSkillMagicActivate());
        this.RegisterListener(EventID.OnSkillSwordActivate,(param)=> OnSkillSwordActivate());
        
        this.RegisterListener(EventID.OnSkillGuardCdFinish,(param)=> OnSkillGuardCdFinish());
        this.RegisterListener(EventID.OnSkillMagicCdDFinish,(param)=> OnSkillMagicCdFinish());
        this.RegisterListener(EventID.OnSkillSwordCdDFinish,(param)=> OnSkillSwordCdFinish());
    }

    public void OnSkillGuardActivate()
    {
        finishCDGuard = false;
        ItemSkillBarGuard.OnSkillActivate();
    }
    
    public void OnSkillMagicActivate()
    {
        finishCDMagic = false;
        ItemSkillBarMagic.OnSkillActivate();
    }
    
    public void OnSkillSwordActivate()
    {
        finishCDSword = false;
        ItemSkillBarSword.OnSkillActivate();
    }
    
    public void OnSkillGuardCdFinish()
    {
        finishCDGuard = true;
    }
    
    public void OnSkillMagicCdFinish()
    {
        finishCDMagic = true;
    }
    
    public void OnSkillSwordCdFinish()
    {
        finishCDSword = true;
    }

    public void UpdateXpAndLevelPlayer(int xp)
    {
        int lvCurrent = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.Level);
        float xpCurrent = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.Xp);
        float xpNextLevel = DataManager.Instance.DataPlayerXp[lvCurrent+1].Item2;

        xpCurrent += xp;

        while (xpCurrent >= xpNextLevel)
        {
            lvCurrent++;
            xpCurrent -= xpNextLevel;
            xpNextLevel = DataManager.Instance.DataPlayerXp[lvCurrent].Item2;
        }
        DataManager.Instance.SaveDataPrefPlayer(DataManager.EDataPlayerEquip.Level,lvCurrent);
        DataManager.Instance.SaveDataPrefPlayer(DataManager.EDataPlayerEquip.Xp,(int)xpCurrent);
        
        StartCoroutine(FillXpBar(xpCurrent,xpNextLevel));
        tmpLevel.text = lvCurrent.ToString();
    }

    IEnumerator FillXpBar(float xpC, float xpN)
    {
        float duration = 0.5f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            xpBarImage.fillAmount = Mathf.Lerp(xpBarImage.fillAmount, xpC/xpN, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
