using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.UI.Dark;
using UnityEngine;

public class PausePopup : MonoBehaviour
{
    public ModalWindowManager modalWindowManager;

    public void OpenPausePopup()
    {
        modalWindowManager.gameObject.SetActive(true);
        modalWindowManager.ModalWindowIn();
    }

    public void CloseSettingTab()
    {
        StartCoroutine(closeSettingTab());
    }

    IEnumerator closeSettingTab()
    {
        modalWindowManager.ModalWindowOut();
        yield return new WaitForSeconds(1f);
        modalWindowManager.gameObject.SetActive(false);
    }
}
