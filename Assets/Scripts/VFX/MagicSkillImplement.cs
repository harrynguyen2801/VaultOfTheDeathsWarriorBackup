using System.Collections;
using System.Collections.Generic;
using Observer;
using UnityEngine;

public class MagicSkillImplement : MonoBehaviour
{
    void OnEnable()
    {
        ImplementPlayerSkill();
    }

    public void ImplementPlayerSkill()
    {
        MainSceneManager.Instance.player.GetComponent<VFXPlayerController>().PlayMagicSkill();
        this.PostEvent(EventID.OnSkillMagicActivate);
    }
}
