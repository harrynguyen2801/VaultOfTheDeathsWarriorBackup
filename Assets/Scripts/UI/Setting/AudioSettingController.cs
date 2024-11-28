using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingController : MonoBehaviour
{
    public Slider volumeBgmSlider;
    public Slider volumeSfxSlider;

    private void OnEnable()
    {
        if (DataManager.Instance != null)
        {
            volumeBgmSlider.value = DataManager.Instance.GetFloatDataPrefGame(DataManager.EDataPrefName.BgVfxVolume);
            SetVolumeBgm();
            volumeSfxSlider.value = DataManager.Instance.GetFloatDataPrefGame(DataManager.EDataPrefName.SoundVfxVolume);
            SetVolumeSfx();
        }
    }

    public void SetVolumeBgm()
    {
        SoundManager.Instance.SetVolumeBgm(volumeBgmSlider.value);
        DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.BgVfxVolume, volumeBgmSlider.value);
    }
    
    public void SetVolumeSfx()
    {
        SoundManager.Instance.SetVolumeSfx(volumeSfxSlider.value);
        DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.SoundVfxVolume, volumeSfxSlider.value);
    }

    public void MuteBgm()
    {
        SoundManager.Instance.MuteBGM();
    }
    
    public void MuteSfx()
    {
        SoundManager.Instance.MuteSfx();
    }
}
