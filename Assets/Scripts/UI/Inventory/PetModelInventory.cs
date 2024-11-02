using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetModelInventory : MonoBehaviour
{   
    public List<GameObject> listPetModels = new List<GameObject>();

    private void OnEnable()
    {
        ActionManager.OnUpdatePetInventoryModelView += ActivePetModel;
    }

    private void OnDisable()
    {
        ActionManager.OnUpdatePetInventoryModelView -= ActivePetModel;
    }

    private void Start()
    {
        DataManager.Instance.LoadDictDataPet();
        int petIdx = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PetId);
        if (petIdx != 0)
        {
            ActivePetModel(petIdx, DataManager.Instance.PetData[petIdx].Item2);
        }

    }

    private void ActivePetModel(int petID, int petLV)
    {
        Debug.Log("pet id " + petID + " pet lv " + petLV);
        ClearPetModels();
        var petIndexCurrentActive = (petID - 1) * 3 + petLV - 1;
        listPetModels[petIndexCurrentActive].SetActive(true);
    }

    private void ClearPetModels()
    {
        listPetModels.ForEach(x => x.SetActive(false));
    }
}
