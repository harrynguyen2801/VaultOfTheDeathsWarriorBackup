using UnityEngine;

public interface IDamageable
{
    void ApplyDamage(float dmg, Vector3 posAttack = new Vector3());
}