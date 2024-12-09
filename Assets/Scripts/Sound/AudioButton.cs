using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public EnumManager.EButtonType buttonType;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySfxButton(EnumManager.ESfxSoundName.Hover);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SoundManager.Instance != null)
        {
            switch (buttonType)
            {
                case EnumManager.EButtonType.CheckBox:
                    SoundManager.Instance.PlaySfxButton(EnumManager.ESfxSoundName.CheckBox);
                    break;
                case EnumManager.EButtonType.ClickBtn:
                    SoundManager.Instance.PlaySfxButton(EnumManager.ESfxSoundName.ClickBtn);
                    break;
                case EnumManager.EButtonType.Hover:
                    SoundManager.Instance.PlaySfxButton(EnumManager.ESfxSoundName.ClickBtn);
                    break;
            }
        }
    }
}
