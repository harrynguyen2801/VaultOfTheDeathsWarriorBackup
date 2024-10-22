using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetEggBroken : MonoBehaviour
{
    public GameObject eggDefault;
    public GameObject eggBroken;

    private void OnEnable()
    {
        eggDefault.SetActive(false);
        eggBroken.SetActive(true);
    }
}
