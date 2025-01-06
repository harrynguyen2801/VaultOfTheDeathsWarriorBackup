using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActiveTimeLine : MonoBehaviour
{
    public GameObject timeline;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeline.SetActive(true);
            timeline.GetComponent<PlayableDirector>().Play();
            MainSceneManager.Instance.player.GetComponent<CharacterController>().enabled = false;
            SoundManager.Instance.PlaySoundBossBgm();
        }
        Destroy(gameObject);
    }
}
