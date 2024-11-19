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

    public ItemPotionBar ItemPotion1Bar;
    public ItemPotionBar ItemPotion2Bar;

    public bool finishCDGuard;
    public bool finishCDMagic;
    public bool finishCDSword;
    
    public bool finishCDPotion1;
    public bool finishCDPotion2;

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
        int lvCurrent = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.LevelPlayer);
        tmpLevel.text = lvCurrent.ToString();

        finishCDGuard = finishCDSword = finishCDMagic = true;
        finishCDPotion1 = finishCDPotion2 = true;

        //Register observer event
        this.RegisterListener(EventID.OnSkillGuardActivate,(param)=> OnSkillGuardActivate());
        this.RegisterListener(EventID.OnSkillMagicActivate,(param)=> OnSkillMagicActivate());
        this.RegisterListener(EventID.OnSkillSwordActivate,(param)=> OnSkillSwordActivate());
        this.RegisterListener(EventID.OnPotion1CdActivate,(param)=> OnPotion1Activate());
        this.RegisterListener(EventID.OnPotion2CdActivate,(param)=> OnPotion2Activate());

        this.RegisterListener(EventID.OnSkillGuardCdFinish,(param)=> OnSkillGuardCdFinish());
        this.RegisterListener(EventID.OnSkillMagicCdDFinish,(param)=> OnSkillMagicCdFinish());
        this.RegisterListener(EventID.OnSkillSwordCdDFinish,(param)=> OnSkillSwordCdFinish());
        this.RegisterListener(EventID.OnPotion1CdFinish,(param)=> OnPotion1CdFinish());
        this.RegisterListener(EventID.OnPotion2CdFinish,(param)=> OnPotion2CdFinish());
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
    
    public void OnPotion1CdFinish()
    {
        finishCDPotion1 = true;
    }
    
    public void OnPotion2CdFinish()
    {
        finishCDPotion2 = true;
    }
    
    public void OnPotion1Activate()
    {
        finishCDPotion1 = false;
        ItemPotion1Bar.OnPotionActivate();
    }
    
    public void OnPotion2Activate()
    {
        finishCDPotion2 = false;
        ItemPotion2Bar.OnPotionActivate();
    }

    public void UpdateXpAndLevelPlayer(int xp)
    {
        int lvCurrent = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.LevelPlayer);
        float xpCurrent = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.Xp);
        float xpNextLevel = DataManager.Instance.DataPlayerXp[lvCurrent+1].Item2;

        xpCurrent += xp;

        while (xpCurrent >= xpNextLevel)
        {
            lvCurrent++;
            xpCurrent -= xpNextLevel;
            xpNextLevel = DataManager.Instance.DataPlayerXp[lvCurrent].Item2;
        }
        DataManager.Instance.SaveDataPrefPlayer(DataManager.EDataPlayerEquip.LevelPlayer,lvCurrent);
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
