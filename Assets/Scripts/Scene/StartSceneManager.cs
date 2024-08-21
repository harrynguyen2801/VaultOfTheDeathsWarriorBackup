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
        if (DataManager.Instance.GetDataInt(DataManager.EDataPrefName.StartScreen) == 0)
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
        // PlayerPrefs.SetInt("PlayerSex",1);
        DataManager.Instance.SaveData(DataManager.EDataPrefName.PlayerSex,1);
        // characterSelect.dissolveSwordFemale.gameObject.SetActive(true);
    }
    public void SelectMalePlayer()
    {
        lightFemale.SetActive(false);
        textNameFemale.gameObject.SetActive(false);
        lightMale.SetActive(true);
        textNameMale.gameObject.SetActive(true);
        animMale.SetTrigger("Attack");
        // PlayerPrefs.SetInt("PlayerSex",0);
        DataManager.Instance.SaveData(DataManager.EDataPrefName.PlayerSex,0);
        // characterSelect.dissolveSwordMale.gameObject.SetActive(true);
    }

    public void SwapSelectToVillageScreen()
    {
        LoadingScreen.Instance.LoadScreen(villageHomeScreen,selectCharacterScreen);
        DataManager.Instance.SaveData(DataManager.EDataPrefName.StartScreen,1);
    }
}
