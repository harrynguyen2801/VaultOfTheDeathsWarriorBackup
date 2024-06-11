using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PropertyHeroScreen : MonoBehaviour
{
    public GameObject maleCharacter;
    public GameObject femaleCharacter;
    public CharacterStartScene CharacterStartScene;
    public InfomationTab InfomationTab;

    private static PropertyHeroScreen _instance;
    public static PropertyHeroScreen Instance => _instance;

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
        if (PlayerPrefs.GetInt("PlayerSex") == 1)
        {
            femaleCharacter.SetActive(true);
            CharacterStartScene = femaleCharacter.GetComponent<CharacterStartScene>();
        }
        else
        {
            maleCharacter.SetActive(true);
            CharacterStartScene = maleCharacter.GetComponent<CharacterStartScene>();
        }
    }

    public void StartGameScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
