using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.VFX;

public class VFXPlayerController : MonoBehaviour
{
    [Header("VFX Player")]
    public ParticleSystem vfxBlade01;
    public ParticleSystem vfxBlade02;
    public ParticleSystem vfxBlade03;
    public ParticleSystem vfxBlade04;
    public ParticleSystem vfxTrailsUp;
    public ParticleSystem vfxTrailsDown;
    public ParticleSystem vfxHealing;
    public VisualEffect vfxFootStep;
    
    
    [Space(30)]
    [Header("Guard Skills")]
    #region GuardSkills

    public ParticleSystem vfxGuard1;
    public ParticleSystem vfxGuard2;
    public ParticleSystem vfxGuard3;

    #endregion
    
    [Space(30)]
    [Header("Magic Skills")]
    #region MagicSkills

    public GameObject vfxMagic1;
    public GameObject vfxMagic2;
    public GameObject vfxMagic3;
    public GameObject vfxMagic4;
    public GameObject vfxMagic5;

    #endregion
    
    [Space(30)]
    [Header("Sword Skills")]
    #region SwordSkills

    public GameObject vfxSword1;
    public GameObject vfxSword2;
    public GameObject vfxSword3;

    #endregion
    Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    public void PlayGuardSkill()
    {
        int skillId = DataManager.Instance.GetUserSkill(DataManager.ESkills.Guard);
        switch (skillId)
        {
            case 1:
                vfxGuard1.gameObject.SetActive(true);
                vfxGuard1.Play();
                break;
            case 2:
                vfxGuard2.gameObject.SetActive(true);
                vfxGuard2.Play();
                break;
            case 3:
                vfxGuard3.gameObject.SetActive(true);
                vfxGuard3.Play();
                break;
        }
    }
    
    public void PlayMagicSkill()
    {
        int skillId = DataManager.Instance.GetUserSkill(DataManager.ESkills.Magic);
        switch (skillId)
        {
            case 1:
                Instantiate(vfxMagic1, player.enemyInRangeSkill, quaternion.identity);
                break;
            case 2:
                Instantiate(vfxMagic2, player.enemyInRangeSkill, quaternion.identity);
                break;
            case 3:
                Instantiate(vfxMagic3, player.transform.position, quaternion.identity);
                break;
            case 4:
                Instantiate(vfxMagic4, player.enemyInRangeSkill, quaternion.identity);
                break;
            case 5:
                Instantiate(vfxMagic5, player.enemyInRangeSkill, quaternion.identity);
                break;
        }
    }
    
    public void PlaySwordSkill()
    {
        int skillId = DataManager.Instance.GetUserSkill(DataManager.ESkills.Sword);
        switch (skillId)
        {
            case 1:
                Instantiate(vfxSword1, player.enemyInRangeSkill, quaternion.identity);
                break;
            case 2:
                Instantiate(vfxSword2, player.enemyInRangeSkill, quaternion.identity);
                break;
            case 3:
                Instantiate(vfxSword3, player.enemyInRangeSkill, vfxSword3.transform.rotation);
                break;
        }
    }

    public void PlayFootStep()
    {
        vfxFootStep.Play();
    }
    
    public void StopFootStep()
    {
        vfxFootStep.Stop();
    }

    public void PlayerVfxHealing()
    {
        vfxHealing.Play();
    }

    public void PlayVfxTrailsUp()
    {
        vfxTrailsUp.Play();
    }
    public void PlayVfxTrailsDown()
    {
        vfxTrailsDown.Play();
    }
    public void PlayVfxBlade01()
    {
        vfxBlade01.Play();
    }
    public void PlayVfxBlade02()
    {
        vfxBlade02.Play();
    }
    public void PlayVfxBlade03()
    {
        vfxBlade03.Play();
    }
    
    public void PlayVfxBlade04()
    {
        vfxBlade04.Play();
    }
    
    public void StopBlade()
    {
        vfxBlade01.Simulate(0);
        vfxBlade01.Stop();
        vfxBlade02.Simulate(0);
        vfxBlade02.Stop();
        vfxBlade03.Simulate(0);
        vfxBlade03.Stop();
        vfxBlade04.Simulate(0);
        vfxBlade04.Stop();
    }
}
