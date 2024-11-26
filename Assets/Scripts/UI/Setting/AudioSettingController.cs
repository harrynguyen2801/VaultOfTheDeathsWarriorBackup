using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingController : MonoBehaviour
{
    private static AudioSettingController _instance;
    public static AudioSettingController Instance => _instance;

    public Slider volumeBgmSlider;
    public Slider volumeSfxSlider;
    
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
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
