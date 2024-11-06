using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DamageCaster : MonoBehaviour
{ 
    private Collider _damageCaster;
    public string targetTag;
    private List<Collider> _damageTargetList;
    public int damage;
    private void Awake()
    {
        _damageCaster = GetComponent<Collider>();
        _damageCaster.enabled = false;
        _damageTargetList = new List<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.GetComponentInParent<Character>().isPlayer)
        {
            damage = MainSceneManager.Instance.player.GetComponent<Player>().damageWeapon;
        }
        // else
        // {
        //     damage = 20;
        // }
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null && !_damageTargetList.Contains(other) && !gameObject.CompareTag(other.tag))
        {
            damageable.ApplyDamage(damage,transform.parent.position);
            _damageTargetList.Add(other);
        }
    }

    public void EnableDamageCaster()
    {
        _damageTargetList.Clear();
        _damageCaster.enabled = true;
    }

    public void DisableDamageCaster()
    {
        _damageTargetList.Clear();
        _damageCaster.enabled = false;
    }
}
