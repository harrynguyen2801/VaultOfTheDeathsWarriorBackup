using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFashionScreen : MonoBehaviour
{
    public GameObject tutorialPanel;
    void Start()
    {
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialVillage) == 0)
        {
            tutorialPanel.SetActive(true);
        }
    }
}
