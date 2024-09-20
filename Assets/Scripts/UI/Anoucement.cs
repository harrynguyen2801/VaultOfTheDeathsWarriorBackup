using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Anoucement : MonoBehaviour
{
    public AlertBox alertBox;
    
    public void ActiveAnoucement()
    {
        SoundManager.Instance.PlaySfx(EnumManager.ESfxSoundName.NotiWarning);
        alertBox.gameObject.SetActive(true);
        alertBox.TweeningAppearAlertBox();
    }
    public void CloseAnoucement()
    {
        StartCoroutine(FadeInAnoucement());
    }

    IEnumerator FadeInAnoucement()
    {
        alertBox.TweeningDeAppearAlertBox();
        yield return new WaitForSeconds(0.2f);
        alertBox.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
