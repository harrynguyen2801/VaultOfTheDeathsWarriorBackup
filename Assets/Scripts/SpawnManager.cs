using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] listSpawnerModel;
    private Spawner[] _listSpawner;

    private void Start()
    {
        _listSpawner = GetComponentsInChildren<Spawner>();
        Debug.Log("length list spawn " + _listSpawner.Length);
        for (int i = 0; i < _listSpawner.Length; i++)
        {
            SpawnModelWithType(_listSpawner[i]);
        }
    }

    private void SpawnModelWithType(Spawner spawner)
    {
        Debug.Log((int)spawner.typeSpawner + " spawnid");
        Instantiate(listSpawnerModel[(int)spawner.typeSpawner], spawner.transform.position, Quaternion.identity);
    }
}
