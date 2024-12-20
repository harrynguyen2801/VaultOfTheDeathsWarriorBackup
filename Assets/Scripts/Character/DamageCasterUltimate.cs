using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DamageCasterUltimate : MonoBehaviour
{ 
    private Collider _damageCaster;
    public string targetTag;
    private List<Collider> _damageTargetList;
    public int damage = 30;
    public DataManager.ESkills eSkills;
    private void Awake()
    {
        _damageCaster = GetComponent<Collider>();
        _damageCaster.enabled = false;
        _damageTargetList = new List<Collider>();
        int idSkill = DataManager.Instance.GetUserSkill(eSkills);
        damage = DataManager.Instance.GetSkillDataByID(idSkill,eSkills).Item5;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        IDamageable damageable = other.GetComponent<IDamageable>();
        // Debug.Log(other.name);
        if (damageable != null && !_damageTargetList.Contains(other) && other.CompareTag(targetTag))
        {
            damageable.ApplyDamage(damage,transform.position);
            _damageTargetList.Add(other);
        }
    }

    private void OnEnable()
    {
        EnableDamageCaster();
    }

    private void OnDisable()
    {
        DisableDamageCaster();
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
