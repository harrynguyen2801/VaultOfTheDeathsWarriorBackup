using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBoxItem : MonoBehaviour
{
    [SerializeField]
    private bool isOn;
    public Image imgChecked;

    private void Start()
    {
        SetToggleState();
    }

    public void SetIsOn(bool isChecked)
    {
        isOn = isChecked;
        SetToggleState();
    }

    public void SetToggleState()
    {
        imgChecked.enabled = isOn;
    }
    
    public void SetToggle()
    {
        isOn = !isOn;
        SetToggleState();
    }
}
