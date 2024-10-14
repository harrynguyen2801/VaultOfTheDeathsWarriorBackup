using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetModelContainer : MonoBehaviour
{
    public List<GameObject> listPetModels = new List<GameObject>();
    public static PetModelContainer Instance => _instance;
    private static PetModelContainer _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
