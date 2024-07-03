using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonRedAttack : MonoBehaviour
{
    public ParticleSystem vfxFlameThrower;

    public void AttackFlameThrower()
    {
        Debug.Log("play flame attack");
        vfxFlameThrower.Play();
    }
    
    
}
