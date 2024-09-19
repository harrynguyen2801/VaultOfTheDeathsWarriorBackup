using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance => _instance;
    private static SoundManager _instance;
    
    public Sound[] bgmSounds, sfxSounds;

    [Space(10)]
    public AudioSource bgmSource, sfxSource;

    #region DataSoundsName
    
    public Dictionary<EnumManager.EBgmSoundName,string> BGMSoundNames = new Dictionary<EnumManager.EBgmSoundName, string>()
    {
        { EnumManager.EBgmSoundName.AssassinCreed ,"AssassinCreed"},
        { EnumManager.EBgmSoundName.MildFlight ,"MildFlight"},
    };
    
    public Dictionary<EnumManager.ESfxSoundName,string> SfxSoundNames = new Dictionary<EnumManager.ESfxSoundName, string>()
    {
        { EnumManager.ESfxSoundName.ClickBtn ,"SfxClickBtn"},
        { EnumManager.ESfxSoundName.CheckBox ,"SfxCheckBox"},

    };

    #endregion
    
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

    public void PlayBgm(EnumManager.EBgmSoundName name)
    {
        Sound s = Array.Find(bgmSounds, x => x.soundName == BGMSoundNames[name]);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            bgmSource.clip = s.clipSound;
            bgmSource.Play();
        }
    }
    
    public void PlaySfx(EnumManager.ESfxSoundName name)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundName == SfxSoundNames[name]);
        if (s == null)
        {
            Debug.Log("Sfx Not Found");
        }
        else
        {
            sfxSource.clip = s.clipSound;
            sfxSource.Play();
        }
    }

    public void SetVolumeBgm(float volume)
    {
        bgmSource.volume = volume;
    }
    
    public void SetVolumeSfx(float volume)
    {
        sfxSource.volume = volume;
    }

    public void MuteBGM()
    {
        bgmSource.mute = !bgmSource.mute;
    }
    
    public void MuteSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }
}
