using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotationPlayerTimeline : MonoBehaviour
{
    public GameObject player;
    private void Update()
    {
        player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
