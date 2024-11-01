using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavContentPet : MonoBehaviour
{
    public GameObject content;

    public GameObject pbPet;

    public List<GameObject> listPetItem = new List<GameObject>();

    private void Start()
    {
        ShowPetListInventory();
    }

    public void ShowPetListInventory()
    {
        DataManager.Instance.LoadDictDataPet();
        listPetItem.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }

        foreach (var data in DataManager.Instance.PetData)
        {
            if (data.Value.Item7 == 1)
            {
                var pet = Instantiate(pbPet,content.transform);
                pet.GetComponent<PetInventoryItem>().SetDataPet(data.Key,data.Value);
                pet.GetComponent<PetInventoryItem>().ShowItem(0.1f);
                listPetItem.Add(pet);
            }
        }

        StartCoroutine(SetChoosePetEquip());
    }

    IEnumerator SetChoosePetEquip()
    {
        yield return new WaitForSeconds(.2f);
        var id = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PetId);
        for (int i = 0; i < content.transform.childCount; i++)
        {
            if (id == content.transform.GetChild(i).GetComponent<PetInventoryItem>().petId)
            {
                content.transform.GetChild(i).GetComponent<PetInventoryItem>().ChoosePet();
            }
        }
    }
    // public void SetChoosePetEquip()
    // {
    //     var id = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PetId);
    //     for (int i = 0; i < content.transform.childCount; i++)
    //     {
    //         if (id == content.transform.GetChild(i).GetComponent<PetInventoryItem>().petId)
    //         {
    //             content.transform.GetChild(i).GetComponent<PetInventoryItem>().ChoosePet();
    //         }
    //     }
    // }
}
