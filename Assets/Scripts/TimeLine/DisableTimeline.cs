using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTimeline : MonoBehaviour
{
    public GameObject timeline;
    void OnEnable()
    {
        timeline.SetActive(false);
        MainSceneManager.Instance.player.GetComponent<CharacterController>().enabled = true;
    }
}
