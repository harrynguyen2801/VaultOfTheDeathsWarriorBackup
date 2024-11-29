using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.UI.Dark;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PausePopup : MonoBehaviour
{
    public ModalWindowManager modalWindowManager;
    private bool _isPaused = false;
    public void OpenPausePopup()
    {
        modalWindowManager.gameObject.SetActive(true);
        modalWindowManager.ModalWindowIn();
        Pause();
    }

    public void CloseSettingTab()
    {
        StartCoroutine(closeSettingTab());
    }

    IEnumerator closeSettingTab()
    {
        modalWindowManager.ModalWindowOut();
        Resume();
        yield return new WaitForSeconds(1f);
        modalWindowManager.gameObject.SetActive(false);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        _isPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0;
        _isPaused = true;
        EventSystem.current.UpdateModules();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        LoadingScreen.Instance.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
