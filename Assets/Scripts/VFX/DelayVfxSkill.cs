using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayVfxSkill : MonoBehaviour
{
    private Collider _damageCaster;
    public float delayTime;
    private void Awake()
    {
        _damageCaster = GetComponent<Collider>();
        _damageCaster.enabled = false;
    }

    IEnumerator DelayDamageCaster()
    {
        yield return new WaitForSeconds(delayTime);
        _damageCaster.enabled = true;
    }
}
