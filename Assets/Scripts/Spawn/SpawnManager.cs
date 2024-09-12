using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] listSpawnerModel;
    private Spawner[] _listSpawner;
    private GameObject _enemyParent;

    private void Start()
    {
        _enemyParent = MainSceneManager.Instance.enemySpawn;
        _listSpawner = GetComponentsInChildren<Spawner>();
        for (int i = 0; i < _listSpawner.Length; i++)
        {
            SpawnModelWithType(_listSpawner[i]);
        }
    }

    private void SpawnModelWithType(Spawner spawner)
    {
        Instantiate(listSpawnerModel[(int)spawner.typeSpawner], spawner.transform.position, Quaternion.identity,_enemyParent.transform);
    }
}
