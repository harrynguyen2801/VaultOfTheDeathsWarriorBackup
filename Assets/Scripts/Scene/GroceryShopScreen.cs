using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroceryShopScreen : MonoBehaviour
{
    public GameObject tutorialPanel;

    private void Start()
    {
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialVillage) == 0)
        {
            tutorialPanel.SetActive(true);
        }
    }
}
