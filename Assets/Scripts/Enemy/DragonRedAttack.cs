using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonRedAttack : MonoBehaviour
{
    public ParticleSystem vfxFlameThrower;
    public AudioSource audioFlameThrower;
    public AudioClip audioFlameCallMeteor;
    public void AttackFlameThrower()
    {
        Debug.Log("play flame attack");
        vfxFlameThrower.Play();
    }

    public void PlayAudioFlameThrower()
    {
        audioFlameThrower.PlayOneShot(audioFlameCallMeteor);
    }
    
}
