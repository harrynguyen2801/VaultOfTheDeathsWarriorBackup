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
            int lv = DataManager.Instance.GetDataInt(DataManager.EDataPrefName.LevelPlay);
            DataManager.Instance.SaveData(DataManager.EDataPrefName.Level,lv + 1);
            Tuple<int, int> dataLevelStateNew = new Tuple<int, int>(DataManager.Instance.LevelStateData[lv+1].Item1,1);
            DataManager.Instance.LevelStateData[lv+1] = dataLevelStateNew;
            DataManager.Instance.SaveDataLevelState();
            DataManager.Instance.LoadDataLevelState();
        }
    }
    
    IEnumerator waitSecond(float sec, Collider other)
    {
        PlayVFXPortalPort();
        var player = other.GetComponent<Player>();
        yield return new WaitForSeconds(1f);
        player.DissapearPlayerInGame();
        yield return new WaitForSeconds(sec);
        // MainSceneManager.Instance.ShowNextLevel(PlayerPrefs.GetInt("Level") + 1);
        MainSceneManager.Instance.endingScreen.WinGame();
    }
}
