using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelScreenItem : MonoBehaviour
{
    public Image bgFrame;
    public Image fgFrame;
    public Image bgMain;
    public Button btn;
    public GameObject objLock;
    public void ActiveHoverBtn()
    {
        StartCoroutine(ActiveHover());
    }
    
    public void DeactiveHoverBtn()
    {
        StartCoroutine(DeactiveHover());
    }

    IEnumerator ActiveHover()
    {
        float timeElapsed = 0f;
        float timeLerp = .5f;
        while (timeElapsed < timeLerp)
        {
            transform.localScale = 
            Vector3.Lerp(transform.localScale, new Vector3(1.4f, 1.4f, 1.4f), timeElapsed / timeLerp);
            fgFrame.fillAmount = Mathf.Lerp(fgFrame.fillAmount, 1f, timeElapsed / timeLerp);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        yield return null;
    }
    
    IEnumerator DeactiveHover()
    {
        btn.interactable = false;
        fgFrame.fillAmount = 0f;
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        yield return new WaitForSeconds(0.5f);
        btn.interactable = true;
    }

    public void ShowItem(float time, Vector3 position, bool animate, bool locked)
    {
        StartCoroutine(Show(time,position, animate,locked));
    }

    IEnumerator Show(float time, Vector3 localPos, bool animate, bool locked)
    {
        yield return new WaitForSeconds(time);
        if (animate)
        {
            transform.DOLocalMoveY(localPos.y + 220f, 0.75f);
        }
        else
        {
            transform.DOLocalMoveY(localPos.y - 220f, 0.75f);
        }
        bgFrame.DOFade(1f, 1.25f);
        fgFrame.DOFade(1f, 1.25f);
        bgMain.DOFade(1f, 1.25f);

        if (locked)
        {
            objLock.SetActive(true);
        }
        else
        {
            objLock.SetActive(false);
        }
    }
}
