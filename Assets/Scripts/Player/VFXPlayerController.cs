using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXPlayerController : MonoBehaviour
{
    public ParticleSystem vfxBlade01;
    public ParticleSystem vfxBlade02;
    public ParticleSystem vfxBlade03;
    public ParticleSystem vfxBlade04;
    public ParticleSystem vfxTrailsUp;
    public ParticleSystem vfxTrailsDown;
    public ParticleSystem vfxHealing;
    public VisualEffect vfxFootStep;

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
