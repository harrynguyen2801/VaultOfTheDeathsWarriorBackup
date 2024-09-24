using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class GateCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Open gate cut scene");
            Player _player = other.GetComponent<Player>();
            _player.openGateCutScene.SetActive(true);
            _player.openGateCutScene.GetComponent<PlayableDirector>().Play();
        }
    }       
}
