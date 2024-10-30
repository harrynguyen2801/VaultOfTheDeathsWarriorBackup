using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InfomationTab : MonoBehaviour
{
    #region Info Weapon

    public TextMeshProUGUI name;
    public TextMeshProUGUI type;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI mana;
    public TextMeshProUGUI detail;

    #endregion
    
    private void OnEnable()
    {
        ActionManager.OnUpdateInformationWeaponTab += SetInformationWeapon;
    }
    
    private void OnDisable()
    {
        ActionManager.OnUpdateInformationWeaponTab -= SetInformationWeapon;
    }

    public void SetInformationWeapon(int id)
    {
        var data = DataManager.Instance.WeaponsDatas.Single(data => data.Key == id).Value;
        name.text = data.Item1;
        type.text = data.Item2;
        hp.text = data.Item4.ToString();
        atk.text = data.Item3.ToString();
        mana.text = data.Item5.ToString();
        detail.text = data.Item6;
    }
}
