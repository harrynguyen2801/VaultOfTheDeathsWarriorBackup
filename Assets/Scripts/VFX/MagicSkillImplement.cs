using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Observer;
using UnityEngine;

public class MagicSkillImplement : MonoBehaviour, ISkillImplement
{
    void OnEnable()
    {
        ImplementSkill();
    }

    public void ImplementSkill()
    {
        MainSceneManager.Instance.player.GetComponent<VFXPlayerController>().PlayMagicSkill();
        this.PostEvent(EventID.OnSkillMagicActivate);
    }
}
