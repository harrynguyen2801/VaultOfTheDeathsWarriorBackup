using System;
using System.Collections;
using System.Collections.Generic;
using DissolveExample;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    public Animator animMale;
    public Animator animFemale;

    public AudioSource musicBg;

    public GameObject selectCharacterScreen;
    public GameObject villageHomeScreen;

    public GameObject mainCamera;
    
    public CharacterSelect characterSelect;

    public GameObject lightMale;
    public GameObject lightFemale;
    
    public TextMeshProUGUI textNameMale;
    public TextMeshProUGUI textNameFemale;
    

    private void Start()
    {
        var renders = GetComponentsInChildren<Renderer>();
        ActiveScreenGame();
    }

    public void ActiveScreenGame()
    {
        Debug.Log(DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.StartScreen) );
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.StartScreen) == 0)
        {
            selectCharacterScreen.SetActive(true);
            musicBg.Play(0);
        }
        else
        {
            mainCamera.SetActive(false);
            villageHomeScreen.SetActive(true);
        }
    }
    
    public void SelectFemalePlayer()
    {
        lightFemale.SetActive(true);
        textNameFemale.gameObject.SetActive(true);
        animFemale.SetTrigger("Attack");
        lightMale.SetActive(false);
        textNameMale.gameObject.SetActive(false);
        DataManager.Instance.SaveDataPrefPlayer(DataManager.EDataPlayerEquip.PlayerSex,1);
    }
    public void SelectMalePlayer()
    {
        lightFemale.SetActive(false);
        textNameFemale.gameObject.SetActive(false);
        lightMale.SetActive(true);
        textNameMale.gameObject.SetActive(true);
        animMale.SetTrigger("Attack");
        DataManager.Instance.SaveDataPrefPlayer(DataManager.EDataPlayerEquip.PlayerSex,0);
    }

    public void SwapSelectToVillageScreen()
    {
        LoadingScreen.Instance.LoadScreen(villageHomeScreen,selectCharacterScreen);
        DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.StartScreen,1);
        mainCamera.SetActive(false);
    }
}
