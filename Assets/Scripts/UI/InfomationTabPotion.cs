using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InfomationTabPotion : MonoBehaviour
{
    #region Info Weapon

    public TextMeshProUGUI name;
    public TextMeshProUGUI type;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI mana;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI detail;

    #endregion
    
    private void OnEnable()
    {
        ActionManager.OnUpdateInformationPotionTab += SetInformationWeapon;
    }
    
    private void OnDisable()
    {
        ActionManager.OnUpdateInformationPotionTab -= SetInformationWeapon;
    }

    public void SetInformationWeapon(int id)
    {
        var data = DataManager.Instance.PotionsDataDefault.Single(data => data.Key == id).Value;
        name.text = data.Item1;
        type.text = data.Item2;
        hp.text = data.Item3.ToString();
        atk.text = data.Item4.ToString();
        mana.text = data.Item5.ToString();
        speed.text = data.Item6.ToString();
        detail.text = data.Item7;
    }
}
