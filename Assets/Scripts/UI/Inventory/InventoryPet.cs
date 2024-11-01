using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPet : MonoBehaviour
{
    public GameObject updatePetPanel;

    public void ShowUpdatePetPanel()
    {
        updatePetPanel.SetActive(true);
    }
    
    public void DeactiveUpdatePetPanel()
    {
        updatePetPanel.SetActive(false);
    }
}
