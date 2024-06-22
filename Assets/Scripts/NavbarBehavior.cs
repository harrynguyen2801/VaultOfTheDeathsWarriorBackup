using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NavbarBehavior : MonoBehaviour
{
    public TextMeshProUGUI[] textNavbar;
    public GameObject[] listTab;

    public Button btnWeaponEqip;

    private void Start()
    {
        btnWeaponEqip.onClick.Invoke();
    }

    public void ClickToButton(TextMeshProUGUI textClick)
    {
        foreach (var t in textNavbar)
        {
            t.color = new Color32(140,140,140,255);
            t.fontSize = 36f;
        }
        textClick.color = new Color32(255,244,216,255);;
        textClick.fontSize = 45f;
        Debug.Log(textClick.name);
    }

    public void InventoryTabShow()
    {
        foreach (var t in listTab)
        {
            t.SetActive(false);
        }
    }
    
    public void SkillsTabShow()
    {
        
    }
    
    public void WeaponsTabShow()
    {
        
    }
}
