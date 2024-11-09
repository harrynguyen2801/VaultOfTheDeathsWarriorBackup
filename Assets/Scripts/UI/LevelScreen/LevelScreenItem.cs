using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelScreenItem : MonoBehaviour
{
    public GameObject objLock;
    public GameObject objOpen;

    public Image imgBg;
    public TextMeshProUGUI tmpTitle;
    public TextMeshProUGUI tmpDescription;
    
    public void SetItemLevel(int idLevel)
    {
        bool locked = DataManager.Instance.LevelStateData[idLevel].Item2 == 0;
        tmpTitle.text = DataManager.Instance.LevelDataDescriptions[idLevel].Item1;
        tmpDescription.text = DataManager.Instance.LevelDataDescriptions[idLevel].Item2;
        imgBg.sprite = Resources.Load<Sprite>("MenuLevel/BgMain/" + DataManager.Instance.LevelDataDescriptions[idLevel].Item3);
        if (locked)
        {
            objLock.SetActive(true);
        }
        else
        {
            objOpen.SetActive(true);
        }
    }
}
