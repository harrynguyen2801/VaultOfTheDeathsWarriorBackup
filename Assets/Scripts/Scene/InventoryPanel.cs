using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryPanel : MonoBehaviour
{
    public GameObject maleCharacter;
    public GameObject femaleCharacter;
    public CharacterStartScene characterStartScene;
    public InfomationTab InfomationTab;

    private static InventoryPanel _instance;
    public static InventoryPanel Instance => _instance;

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

        tmpCoin.text = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Coin).ToString();
    }
    
}
