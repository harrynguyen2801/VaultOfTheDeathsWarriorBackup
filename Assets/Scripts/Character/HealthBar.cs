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
        sliderHealth.maxValue = _enemy.MaxHealth;
        sliderErase.maxValue = _enemy.MaxHealth;
    }

    private void Update()
    {
        if (_enemy.CurrentHealth != sliderHealth.value)
        {
            sliderHealth.value = _enemy.CurrentHealth;
        }
        
        if (_enemy.CurrentHealth != sliderErase.value)
        {
            sliderErase.value = Mathf.Lerp(sliderErase.value, _enemy.CurrentHealth, 0.05f);
        }
    }
}
