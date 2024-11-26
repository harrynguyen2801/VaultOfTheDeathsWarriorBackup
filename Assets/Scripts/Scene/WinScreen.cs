using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public Image mainBgWin;
    public Image lineLeftWin;
    public Image lineRightWin;
    [SerializeField]        
    private List<Image> starList;
    
    private void WinScreenActive()
    {
        mainBgWin.DOFade(1f, .2f);
        mainBgWin.transform.DOScale(new Vector3(1f, 1f, 1f), .2f);
        for (int i = 0; i < starList.Count; i++)
        {
            DOTween.Sequence().SetDelay(i/2f).Append(starList[i].DOFade(1f, .2f));
            DOTween.Sequence().SetDelay(i/2f).Append(starList[i].transform.DOScale(new Vector3(1f, 1f, 1f), .2f));
        }
        StartCoroutine(FillImagedWin());
        int lv = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.LevelOpen);
        int lvPlay = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.LevelPlay);
        if (lv == lvPlay && lv < 3)
        {
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.LevelOpen,lv + 1);
            Tuple<int, int> dataLevelStateNew = new Tuple<int, int>(DataManager.Instance.LevelStateData[lv+1].Item2,1);
            DataManager.Instance.LevelStateData[lv+1] = dataLevelStateNew;
            DataManager.Instance.SaveDataLevelState();
            DataManager.Instance.LoadDataLevelState();
        }
    }

    private float durations = .65f;
    IEnumerator FillImagedWin()
    {
        float t = 0f;
        while (t < durations)
        {
            lineLeftWin.fillAmount = Mathf.Lerp(0f, 1f, t / durations);
            lineRightWin.fillAmount = Mathf.Lerp(0f, 1f, t / durations);
            t += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator ActiveWinScreen(string animName)
    {
        Animator anim = GetComponent<Animator>();
        AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(0);
        while (animState.IsName(animName) && animState.normalizedTime >= 1.0f)
        {
            animState = anim.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        yield return new WaitForSeconds(.75f);
        anim.SetTrigger("Trans");
        yield return new WaitForSeconds(.3f);
        WinScreenActive();
    }
    
    private void OnEnable()
    {
        // WinScreenActive();
        StartCoroutine(ActiveWinScreen("BGDissolveOut"));
    }
}
