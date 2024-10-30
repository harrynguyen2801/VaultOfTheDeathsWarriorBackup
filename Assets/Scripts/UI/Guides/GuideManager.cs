using System.Collections;
using System.Collections.Generic;
using Michsky.UI.Dark;
using UnityEngine;

public class GuideManager : MonoBehaviour
{
    public PanelTabManager guideTab;

    public void OpenGuideTab()
    {
        guideTab.gameObject.SetActive(true);
    }

    public void CloseGuideTab()
    {
        StartCoroutine(closeGuideTab());
    }

    IEnumerator closeGuideTab()
    {
        guideTab.GetComponent<Animator>().Play(guideTab.panelFadeOut);
        yield return new WaitForSeconds(1f);
        guideTab.gameObject.SetActive(false);
    }
}
