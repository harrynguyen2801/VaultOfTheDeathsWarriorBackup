using System.Collections;
using System.Collections.Generic;
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

    public ParticleSystem vfxMagic1;
    public ParticleSystem vfxMagic2;
    public ParticleSystem vfxMagic3;
    public ParticleSystem vfxMagic4;
    public ParticleSystem vfxMagic5;

    #endregion
    
    [Space(30)]
    [Header("Sword Skills")]
    #region SwordSkills

    public ParticleSystem vfxSword1;
    public ParticleSystem vfxSword2;
    public ParticleSystem vfxSword3;

    #endregion

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
                vfxMagic1.gameObject.SetActive(true);
                vfxMagic1.Play();
                break;
            case 2:
                vfxMagic2.gameObject.SetActive(true);
                vfxMagic2.Play();
                break;
            case 3:
                vfxMagic3.gameObject.SetActive(true);
                vfxMagic3.Play();
                break;
            case 4:
                vfxMagic4.gameObject.SetActive(true);
                vfxMagic4.Play();
                break;
            case 5:
                vfxMagic5.gameObject.SetActive(true);
                vfxMagic5.Play();
                break;
        }
    }
    
    public void PlaySwordSkill()
    {
        int skillId = DataManager.Instance.GetUserSkill(DataManager.ESkills.Sword);
        switch (skillId)
        {
            case 1:
                vfxSword1.gameObject.SetActive(true);
                vfxSword1.Play();
                break;
            case 2:
                vfxSword2.gameObject.SetActive(true);
                vfxSword2.Play();
                break;
            case 3:
                vfxSword3.gameObject.SetActive(true);
                vfxSword3.Play();
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
