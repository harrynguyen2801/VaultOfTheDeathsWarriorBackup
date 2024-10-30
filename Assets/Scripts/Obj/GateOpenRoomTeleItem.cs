using System;
using System.Collections;
using System.Collections.Generic;
using Observer;
using Unity.Mathematics;
using UnityEngine;

public class GateOpenRoomTele : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            int level = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.LevelPlay);
            MainSceneManager.Instance.player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = MainSceneManager.Instance.levelList[level-1].GetComponent<GameLevelManager>().playerOpenGatePosition.position;
            player.transform.Rotate(0f, 0f, 0f);
            Debug.Log(player.transform.rotation);
            player.openGateCutScene.SetActive(true);
        }
        Destroy(gameObject);
    }
}
