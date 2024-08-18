using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;

public class GuardSkillImplement : MonoBehaviour
{
    void OnEnable()
    {
        ImplementPlayerSkill();
    }

    public void ImplementPlayerSkill()
    {
        MainSceneManager.Instance.player.GetComponent<VFXPlayerController>().PlayGuardSkill();
        this.PostEvent(EventID.OnSkillGuardActivate);
    }
}
