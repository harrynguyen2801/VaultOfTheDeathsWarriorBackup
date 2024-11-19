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
            MainSceneManager.Instance.endingScreen.WinGame();
        }
    }
    
    IEnumerator waitSecond(float sec, Collider other)
    {
        PlayVFXPortalPort();
        var player = other.GetComponent<Player>();
        yield return new WaitForSeconds(1f);
        player.DissapearPlayerInGame();
        yield return new WaitForSeconds(sec);
        // MainSceneManager.Instance.ShowNextLevel(PlayerPrefs.GetInt("LevelOpen") + 1);
        MainSceneManager.Instance.endingScreen.WinGame();
    }
}
