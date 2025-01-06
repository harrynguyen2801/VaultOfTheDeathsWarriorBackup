using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance => _instance;
    private static SoundManager _instance;
    
    public Sound[] bgmSounds, sfxSounds, playerAndEnemySounds;

    [Space(10)]
    public AudioSource bgmSource, sfxSource, playerSource, enemySource, bossSource;

    #region DataSoundsName
    
    public Dictionary<EnumManager.EBgmSoundName,string> BGMSoundNames = new Dictionary<EnumManager.EBgmSoundName, string>()
    {
        { EnumManager.EBgmSoundName.AssassinCreed ,"AssassinCreed"},
        { EnumManager.EBgmSoundName.MildFlight ,"MildFlight"},
        { EnumManager.EBgmSoundName.DungeonLoop ,"DungeonLoop"},
    };
    
    public Dictionary<EnumManager.ESfxSoundName,string> ButtonSfxSoundNames = new Dictionary<EnumManager.ESfxSoundName, string>()
    {
        { EnumManager.ESfxSoundName.ClickBtn ,"SfxClickBtn"},
        { EnumManager.ESfxSoundName.CheckBox ,"SfxCheckBox"},
        { EnumManager.ESfxSoundName.NotiError ,"SfxNotiError"},
        { EnumManager.ESfxSoundName.NotiWarning ,"SfxNotiWarning"},
        { EnumManager.ESfxSoundName.NotiAlert ,"SfxNotiAlert"},
        { EnumManager.ESfxSoundName.SwordSlash ,"SfxSwordSlash"},
        { EnumManager.ESfxSoundName.Hover ,"SfxHover"},
        { EnumManager.ESfxSoundName.LightMagic ,"SfxLightMagic"},
    };
    
    public Dictionary<EnumManager.ESfxObjType,string> ObjSfxSoundNames = new Dictionary<EnumManager.ESfxObjType, string>()
    {
        { EnumManager.ESfxObjType.HealObj ,"SfxHealObj"},
        { EnumManager.ESfxObjType.CoinObj ,"SfxCoinObj"},
    };
    
    public Dictionary<EnumManager.ESkillSfxType,string> SkillSfxSoundNames = new Dictionary<EnumManager.ESkillSfxType, string>()
    {
        { EnumManager.ESkillSfxType.Guard ,"SfxGuard"},
        { EnumManager.ESkillSfxType.Magic ,"SfxMagic"},
        { EnumManager.ESkillSfxType.Sword ,"SfxSword"},
    };
    
    public Dictionary<EnumManager.ESfxSoundPlayer,string> PlayerSfxSoundNames = new Dictionary<EnumManager.ESfxSoundPlayer, string>()
    {
        { EnumManager.ESfxSoundPlayer.Hit ,"SfxHit"},
        { EnumManager.ESfxSoundPlayer.Defend ,"SfxDefend"},
        { EnumManager.ESfxSoundPlayer.SwordSlash ,"SfxSwordSlash"},
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

    private void Start()
    {
        SetVolumeBgm(DataManager.Instance.GetFloatDataPrefGame(DataManager.EDataPrefName.MusicVolume));
        SetVolumeSfx(DataManager.Instance.GetFloatDataPrefGame(DataManager.EDataPrefName.SoundVolume));
        SetVolumeSfxEnemy(DataManager.Instance.GetFloatDataPrefGame(DataManager.EDataPrefName.SoundVolume));
        SetVolumeSfxPlayer(DataManager.Instance.GetFloatDataPrefGame(DataManager.EDataPrefName.SoundVolume));
        SetVolumeBossBgm(DataManager.Instance.GetFloatDataPrefGame(DataManager.EDataPrefName.SoundVolume));
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
    
    public void PlaySfxButton(EnumManager.ESfxSoundName name)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundName == ButtonSfxSoundNames[name]);
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
    
    public void PlaySfxObj(EnumManager.ESfxObjType name)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundName == ObjSfxSoundNames[name]);
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
    
    public void PlaySfxSkill(EnumManager.ESkillSfxType name)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundName == SkillSfxSoundNames[name]);
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
    
    public void PlaySfxPlayer(EnumManager.ESfxSoundPlayer name)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundName == PlayerSfxSoundNames[name]);
        if (s == null)
        {
            Debug.Log("Sfx Not Found");
        }
        else
        {
            playerSource.clip = s.clipSound;
            playerSource.Play();
        }
    }
    
    public void PlaySfxEnemy(EnumManager.ESfxSoundPlayer name)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundName == PlayerSfxSoundNames[name]);
        if (s == null)
        {
            Debug.Log("Sfx Not Found");
        }
        else
        {
            enemySource.clip = s.clipSound;
            enemySource.Play();
        }
    }

    public void PlaySoundBossBgm()
    {
        bossSource.Play();
    }
    
    public void StopSoundBossBgm()
    {
        if (bossSource != null && bossSource.isPlaying)
        {
            StartCoroutine(FadeOutCoroutine());
        }
    }

    public void SetVolumeBgm(float volume)
    {
        bgmSource.volume = volume;
    }
    
    public void SetVolumeBossBgm(float volume)
    {
        
        bossSource.volume = volume;
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = bossSource.volume;

        for (float t = 0; t < 2f; t += Time.deltaTime)
        {
            bossSource.volume = Mathf.Lerp(startVolume, 0, t / 2f);
            yield return null;
        }

        bossSource.volume = 0;
        bossSource.Stop();
    }
    
    public void SetVolumeSfx(float volume)
    {
        sfxSource.volume = volume;
    }
    
    public void SetVolumeSfxPlayer(float volume)
    {
        playerSource.volume = volume;
    }
    
    public void SetVolumeSfxEnemy(float volume)
    {
        enemySource.volume = volume;
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
