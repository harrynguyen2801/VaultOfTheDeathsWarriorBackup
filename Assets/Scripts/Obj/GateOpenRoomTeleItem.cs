using System;
using System.Collections;
using System.Collections.Generic;
using Observer;
using Unity.Mathematics;
using UnityEngine;

public class GateOpenRoomTele : MonoBehaviour
{
    Player player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            int level = DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.LevelPlay);
            MainSceneManager.Instance.player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = MainSceneManager.Instance.levelList[level-1].GetComponent<GameLevelManager>().playerOpenGatePosition.position;
            Debug.Log(player.transform.rotation);
            player.openGateCutScene.SetActive(true);
        }
        Destroy(gameObject);
    }
}
