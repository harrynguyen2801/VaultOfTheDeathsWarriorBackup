using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTimeLine : MonoBehaviour
{
    public GameObject timeline;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeline.SetActive(true);
            GameManager.Instance.player.GetComponent<CharacterController>().enabled = false;
        }
    }
}
