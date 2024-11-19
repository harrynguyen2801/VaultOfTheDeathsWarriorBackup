using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionItemImg : MonoBehaviour
{
    private Image _imgMainPotion;

    private void OnEnable()
    {
        ActionManager.OnUpdateInformationPotionTab += ShowMainPotion;
    }

    private void OnDisable()
    {
        ActionManager.OnUpdateInformationPotionTab -= ShowMainPotion;
    }

    private void Awake()
    {
        _imgMainPotion = GetComponent<Image>();
    }

    private void ShowMainPotion(int potionIndex)
    {
        StartCoroutine(ShowPotion(potionIndex));
    }

    IEnumerator ShowPotion(int potionIndex)
    {
        _imgMainPotion.sprite = Resources.Load<Sprite>("Potion/" + potionIndex);
        _imgMainPotion.materialForRendering.SetFloat("_FullGlowDissolveFade", 0f);
        // float valDissolve = _imgMainPotion.materialForRendering.GetFloat("_FullGlowDissolveFade");
        float _showTime = .4f;
        float _lerpTime = 0f;
        while (_lerpTime < _showTime)
        {
            _lerpTime += Time.deltaTime;
            float currentValue = Mathf.Lerp(0f,1f,_lerpTime/_showTime);
            _imgMainPotion.materialForRendering.SetFloat("_FullGlowDissolveFade", currentValue);
            yield return null;
        }
    }
    
    IEnumerator HidePotion()
    {
        float valDissolve = _imgMainPotion.materialForRendering.GetFloat("_FullGlowDissolveFade");
        float _hideTime = .5f;
        float _lerpTime = 0f;
        while (_lerpTime < _hideTime)
        {
            _lerpTime += Time.deltaTime;
            float currentValue = Mathf.Lerp(valDissolve,0f,_lerpTime/_hideTime);
            _imgMainPotion.materialForRendering.SetFloat("_FullGlowDissolveFade", currentValue);
            yield return null;
        }
    }
}
