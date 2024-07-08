using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            Character cc = other.gameObject.GetComponent<Character>();
            if (damageable != null && cc != null && cc.isPlayer)
            {
                damageable.ApplyDamage(5);
            }
        }
    }
}
