using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider sliderErase;
    public Slider sliderHealth;
    private Enemy _enemy;
    
    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        sliderHealth.maxValue = _enemy.GetMaxHealth();
        sliderErase.maxValue = _enemy.GetMaxHealth();
    }

    private void Update()
    {
        if (!Mathf.Approximately(_enemy.GetCurrentHealth(), sliderHealth.value))
        {
            sliderHealth.value = _enemy.GetCurrentHealth();
        }
        
        if (!Mathf.Approximately(_enemy.GetCurrentHealth(), sliderErase.value))
        {
            sliderErase.value = Mathf.Lerp(sliderErase.value, _enemy.GetCurrentHealth(), 0.05f);
        }
    }
}
