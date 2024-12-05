using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public EnumManager.EButtonType buttonType;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySfx(EnumManager.ESfxSoundName.Hover);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SoundManager.Instance != null)
        {
            switch (buttonType)
            {
                case EnumManager.EButtonType.CheckBox:
                    SoundManager.Instance.PlaySfx(EnumManager.ESfxSoundName.CheckBox);
                    break;
                case EnumManager.EButtonType.ClickBtn:
                    SoundManager.Instance.PlaySfx(EnumManager.ESfxSoundName.ClickBtn);
                    break;
                case EnumManager.EButtonType.Hover:
                    SoundManager.Instance.PlaySfx(EnumManager.ESfxSoundName.ClickBtn);
                    break;
            }
        }
    }
}
