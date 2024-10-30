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
    private static PropertyHeroScreen _instance;
    public static PropertyHeroScreen Instance => _instance;

    public TextMeshProUGUI tmpCoin;

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
        if (DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PlayerSex) == 1)
        {
            femaleCharacter.SetActive(true);
            characterStartScene = femaleCharacter.GetComponent<CharacterStartScene>();
        }
        else
        {
            maleCharacter.SetActive(true);
            characterStartScene = maleCharacter.GetComponent<CharacterStartScene>();
        }

        UpdateCoin();
    }

    public void UpdateCoin()
    {
        tmpCoin.text = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin).ToString();
    }
    
}
