using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Anoucement : MonoBehaviour
{
    public AlertBox alertBox;
    public TextMeshProUGUI tmpText;
    public GameObject canvas;
    private void OnEnable()
    {
        ActionManager.OnUpdateAnoucement += ActiveAnoucement;
    }
    
    private void OnDisable()
    {
        ActionManager.OnUpdateAnoucement -= ActiveAnoucement;
    }
    public void ActiveAnoucement(string anoucement)
    {
        canvas.SetActive(true);
        tmpText.text = anoucement;
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
        canvas.SetActive(false);
    }
}
