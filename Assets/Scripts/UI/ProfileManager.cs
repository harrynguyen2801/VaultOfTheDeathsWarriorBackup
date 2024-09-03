using System;
using System.Collections;
using System.Collections.Generic;
using Observer;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    public Text name;
    public Text level;
    public Image avatar;
    public Slider healthBar;
    public Slider manaBarErase;
    public Slider healthBarErase;
    public Slider manaBar;
    
    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        healthBar.maxValue = _player.GetMaxHealth();
        healthBarErase.maxValue = _player.GetMaxHealth();
        
        manaBar.maxValue = _player.GetMaxMana();
        manaBarErase.maxValue = _player.GetMaxMana();
        
        // healthBar.value = _player.GetMaxHealth();
        // healthBarErase.value = _player.GetMaxHealth();
        //
        // manaBar.value = _player.GetMaxMana();
        // manaBarErase.value = _player.GetMaxMana();
    }

    public void SetProfile(string _name, string _avatarId, string _level)
    {
        name.text = _name;
        // avatar.sprite = 
        level.text = _level;
    }

    private void Update()
    {
        if (!Mathf.Approximately(_player.GetCurrentHealth(), healthBar.value))
        {
            healthBar.value = _player.GetCurrentHealth();
        }
        if (!Mathf.Approximately(_player.GetCurrentMana(),manaBar.value))
        {
            manaBar.value = _player.GetCurrentMana();
        }
        //
        // if (!Mathf.Approximately(_player.GetCurrentHealth(),healthBarErase.value))
        // {
        //     healthBarErase.value = Mathf.Lerp(healthBarErase.value, _player.GetCurrentHealth(), 0.05f);
        // }
        // if (!Mathf.Approximately(_player.GetCurrentMana(), manaBarErase.value))
        // {
        //     manaBarErase.value = Mathf.Lerp(manaBarErase.value, _player.GetCurrentMana(), 0.05f);
        // }
    }
}
