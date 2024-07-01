using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfomationTab : MonoBehaviour
{
    #region Info Weapon

    public TextMeshProUGUI name;
    public TextMeshProUGUI type;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI def;
    public TextMeshProUGUI mana;
    public TextMeshProUGUI detail;

    #endregion

    public void SetInformationWeapon(Tuple<string, string, int, int, int, int, string, Tuple<int>> data)
    {
        name.text = data.Item1;
        type.text = data.Item2;
        hp.text = data.Item3.ToString();
        atk.text = data.Item4.ToString();
        def.text = data.Item5.ToString();
        mana.text = data.Item6.ToString();
        detail.text = data.Item7;
    }
}
