using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTimeline : MonoBehaviour
{
    public GameObject timeline;
    public GameObject activeTimeline;
    void OnEnable()
    {
        timeline.SetActive(false);
        GameManager.Instance.player.GetComponent<Character_Input>().enabled = true;
        activeTimeline.SetActive(false);
    }
}
