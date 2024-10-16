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
        if (DataManager.Instance.GetDataInt(DataManager.EDataPrefName.PlayerSex) == 1)
        {
            femaleCharacter.SetActive(true);
            characterStartScene = femaleCharacter.GetComponent<CharacterStartScene>();
        }
        else
        {
            maleCharacter.SetActive(true);
            characterStartScene = maleCharacter.GetComponent<CharacterStartScene>();
        }

        tmpCoin.text = DataManager.Instance.GetDataInt(DataManager.EDataPrefName.Coin).ToString();
    }
    
}
