using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InformationSkills : MonoBehaviour
{
    #region Info Skill

    public TextMeshProUGUI name;
    public TextMeshProUGUI type;
    public TextMeshProUGUI cd;
    public TextMeshProUGUI mana;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI detail;

    #endregion
    
    public void SetInformationSkills(Tuple<string,int,int,int,int,string,int> data)
    {
        name.text = data.Item1;
        type.text = "Ultimate";
        atk.text = data.Item2.ToString();
        cd.text = data.Item3.ToString();
        mana.text = data.Item4.ToString();
        detail.text = data.Item6;
    }
}
