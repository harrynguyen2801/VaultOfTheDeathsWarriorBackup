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

    private void Start()
    {
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
}
