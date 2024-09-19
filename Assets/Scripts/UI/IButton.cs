using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IButton : MonoBehaviour
{
    public EnumManager.EButtonType buttonType;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlaySoundClick);
    }
    
    private void PlaySoundClick()
    {
        switch (buttonType)
        {
            case EnumManager.EButtonType.CheckBox:
                SoundManager.Instance.PlaySfx(EnumManager.ESfxSoundName.CheckBox);
                break;
            case EnumManager.EButtonType.ClickBtn:
                SoundManager.Instance.PlaySfx(EnumManager.ESfxSoundName.ClickBtn);
                break;
        }
    }
}
