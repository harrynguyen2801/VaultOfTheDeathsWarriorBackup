using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVFXManager : MonoBehaviour
{
   public ParticleSystem BeingHitVFX;

   public void PlayerBeingHitVFX(Vector3 attackPos)
   {
      // Vector3 forwardForce = transform.position - attackPos;
      // forwardForce.Normalize();
      // forwardForce.y = 0;
      // transform.rotation = Quaternion.LookRotation(forwardForce);
      BeingHitVFX.Play();
   }
}
