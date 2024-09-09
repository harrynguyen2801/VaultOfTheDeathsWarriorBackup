using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Observer;

public class GuardSkillImplement : MonoBehaviour, ISkillImplement
{
    void OnEnable()
    {
        ImplementSkill();
    }
    
    public void ImplementSkill()
    {
        MainSceneManager.Instance.player.GetComponent<VFXPlayerController>().PlayGuardSkill();
        this.PostEvent(EventID.OnSkillGuardActivate);
    }
}
