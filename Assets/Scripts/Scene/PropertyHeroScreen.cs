using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PropertyHeroScreen : MonoBehaviour
{
    private static PropertyHeroScreen _instance;
    public static PropertyHeroScreen Instance => _instance;

    public TextMeshProUGUI tmpCoin;

    public GameObject tutorial;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }
    
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

        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Tutorial) == 0)
        {
            tutorial.SetActive(true);
        }
    }

    public void UpdateCoin()
    {
        tmpCoin.text = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin).ToString();
    }

    public void UpdateWeapon()
    {
        ActionManager.OnUpdateWeaponPlayer(true, DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId));
    }
}
