using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet3DModelContainer : MonoBehaviour
{
    public GameObject[] petModel;

    private void OnEnable()
    {
        ActivePetWithEgg(VillageHomeScreen.Instance.petIdx);
    }

    public void ActivePetWithEgg(int petID)
    {
        foreach (GameObject pet in petModel)
        {
            pet.SetActive(false);
        }
        petModel[petID-1].SetActive(true);
    }
}
