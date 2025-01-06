using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingController : MonoBehaviour
{
    public Slider volumeBgmSlider;
    public Slider volumeSfxSlider;
    // public Slider volumeMasterSlider;

    private void OnEnable()
    {
        if (DataManager.Instance != null)
        {
            volumeBgmSlider.value = DataManager.Instance.GetFloatDataPrefGame(DataManager.EDataPrefName.MusicVolume);
            SetVolumeBgm();
            volumeSfxSlider.value = DataManager.Instance.GetFloatDataPrefGame(DataManager.EDataPrefName.SoundVolume);
            SetVolumeSfx();
            // volumeMasterSlider.value = DataManager.Instance.GetFloatDataPrefGame(DataManager.EDataPrefName.MasterVolume);
            // SetVolumeMasterSfx();
        }
    }

    public void SetVolumeBgm()
    {
        SoundManager.Instance.SetVolumeBgm(volumeBgmSlider.value);
        SoundManager.Instance.SetVolumeBossBgm(volumeBgmSlider.value);
        DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.MusicVolume, volumeBgmSlider.value);
    }
    
    public void SetVolumeSfx()
    {
        SoundManager.Instance.SetVolumeSfx(volumeSfxSlider.value); 
        SoundManager.Instance.SetVolumeSfxPlayer(volumeSfxSlider.value); 
        SoundManager.Instance.SetVolumeSfxEnemy(volumeSfxSlider.value); 
        DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.SoundVolume, volumeSfxSlider.value);
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerSkillSound>() != null)
            {
                player.GetComponent<PlayerSkillSound>().SetSoundVolume();
            }
        }
    }
    
    // public void SetVolumeMasterSfx()
    // {
    //     SoundManager.Instance.SetVolumeMasterSfx(volumeMasterSlider.value);
    //     DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.MasterVolume, volumeMasterSlider.value);
    // }

    public void MuteBgm()
    {
        SoundManager.Instance.MuteBGM();
    }
    
    public void MuteSfx()
    {
        SoundManager.Instance.MuteSfx();
    }
}
