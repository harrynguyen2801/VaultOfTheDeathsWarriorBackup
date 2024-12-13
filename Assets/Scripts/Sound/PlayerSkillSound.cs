using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSound : MonoBehaviour
{
   public AudioSource guard, magic, sword;
   private float _volume;

   private void Start()
   {
      SetSoundVolume();
   }

   public void SetSoundVolume()
   {
      _volume = DataManager.Instance.GetFloatDataPrefGame(DataManager.EDataPrefName.SoundVolume);
      if (guard != null)
      {
         guard.volume = _volume;
      }
      if (magic != null)
      {
         magic.volume = _volume;
      }
      if (sword != null)
      {
         sword.volume = _volume;
      }
   }
}
