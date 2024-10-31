using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUIManager : MonoBehaviour
{
    public TextMeshProUGUI tmpCoin;


    private void OnEnable()
    {
        ActionManager.OnUpdateCoin += UpdateCoin;
    }

    private void OnDisable()
    {
        ActionManager.OnUpdateCoin -= UpdateCoin;
    }

    void Start()
    {
        UpdateCoin();
    }

    public void UpdateCoin()
    {
        tmpCoin.text = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin).ToString();
    }
}
