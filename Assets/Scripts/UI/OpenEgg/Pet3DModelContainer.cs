using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet3DModelContainer : MonoBehaviour
{
    public GameObject[] petModel;

    private void Start()
    {
        ActivePetWithEgg();
    }

    private void ActivePetWithEgg()
    {
        petModel[0].SetActive(true);
    }
}
