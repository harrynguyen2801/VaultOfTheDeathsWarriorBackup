using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillImplement : MonoBehaviour
{
    void OnEnable()
    {
        ImplementPlayerSkill();
    }

    public void ImplementPlayerSkill()
    {
        MainSceneManager.Instance.player.GetComponent<VFXPlayerController>().PlaySwordSkill();
    }
}
