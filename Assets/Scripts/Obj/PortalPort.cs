using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PortalPort : MonoBehaviour
{
    public VisualEffect portalPort;

    private bool _activeTeleport;

    public void PlayVFXPortalPort()
    {
        portalPort.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(waitSecond(2f,other));
            int levelSave = DataManager.Instance.GetDataInt(DataManager.DataPrefName.Level);
            DataManager.Instance.SaveData(DataManager.DataPrefName.Level,levelSave + 1);
        }
    }
    
    IEnumerator waitSecond(float sec, Collider other)
    {
        PlayVFXPortalPort();
        var player = other.GetComponent<Player>();
        yield return new WaitForSeconds(1f);
        player.DissapearPlayerInGame();
        yield return new WaitForSeconds(sec);
        // GameManager.Instance.ShowNextLevel(PlayerPrefs.GetInt("Level") + 1);
        GameManager.Instance.endingScreen.WinGame();
    }
}
