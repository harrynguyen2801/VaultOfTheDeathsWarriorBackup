using System;
using System.Collections;
using System.Collections.Generic;
using Observer;
using Unity.Mathematics;
using UnityEngine;

public class GateOPenRoomTele : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            int level = DataManager.Instance.GetDataInt(DataManager.EDataPrefName.Level);
            MainSceneManager.Instance.player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<Transform>().position = MainSceneManager.Instance.levelList[level-1].GetComponent<GameLevelManager>().playerOpenGatePosition.position;
            player.GetComponent<Transform>().Rotate(0f, 0f, 0f);
            
            player.openGateCutScene.SetActive(true);
        }
        Destroy(gameObject);
    }
}
