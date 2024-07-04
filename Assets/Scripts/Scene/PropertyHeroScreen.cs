using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PropertyHeroScreen : MonoBehaviour
{
    public GameObject maleCharacter;
    public GameObject femaleCharacter;
    public CharacterStartScene characterStartScene;
    public InfomationTab InfomationTab;

    private static PropertyHeroScreen _instance;
    public static PropertyHeroScreen Instance => _instance;

    public TextMeshProUGUI tmpCoin;
    public Anoucement anoucement;

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

    void Start()
    {
        if (DataManager.Instance.LoadDataInt(DataManager.DataPrefName.PlayerSex) == 1)
        {
            femaleCharacter.SetActive(true);
            characterStartScene = femaleCharacter.GetComponent<CharacterStartScene>();
        }
        else
        {
            maleCharacter.SetActive(true);
            characterStartScene = maleCharacter.GetComponent<CharacterStartScene>();
        }

        DataManager.Instance.SaveData(DataManager.DataPrefName.Coin,500);
        tmpCoin.text = DataManager.Instance.LoadDataInt(DataManager.DataPrefName.Coin).ToString();
    }

    public void NextScene()
    {
        LoadingScreen.Instance.LoadScene("MainScene");
    }
}
