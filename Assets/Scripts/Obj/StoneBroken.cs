using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBroken : MonoBehaviour
{
    public GameObject stoneDefault;
    public GameObject stoneBroken;

    private void OnEnable()
    {
        stoneDefault.SetActive(false);
        stoneBroken.SetActive(true);
    }
}
