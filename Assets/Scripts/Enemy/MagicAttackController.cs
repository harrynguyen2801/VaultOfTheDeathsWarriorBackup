using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttackController : MonoBehaviour
{
    public int damage = 30;
    public float timeDestroy = 5f;
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        Character cc = other.gameObject.GetComponent<Character>();
        if (damageable != null && cc != null && cc.isPlayer)
        {
            damageable.ApplyDamage(damage);
            // _cc.ApplyDamage(damage,transform.position);
        }

        StartCoroutine(DestroyObject(1f));
    }

    IEnumerator DestroyObject(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(DestroyObject(timeDestroy));
    }
}
