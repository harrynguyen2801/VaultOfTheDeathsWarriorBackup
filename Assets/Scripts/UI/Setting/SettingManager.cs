using System.Collections;
using System.Collections.Generic;
using Michsky.UI.Dark;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public PanelTabManager settingTab;

    public void OpenSettingTab()
    {
        settingTab.gameObject.SetActive(true);
    }

    public void CloseSettingTab()
    {
        StartCoroutine(closeSettingTab());
    }

    IEnumerator closeSettingTab()
    {
        settingTab.GetComponent<Animator>().Play(settingTab.panelFadeOut);
        yield return new WaitForSeconds(1f);
        settingTab.gameObject.SetActive(false);
    }
}
