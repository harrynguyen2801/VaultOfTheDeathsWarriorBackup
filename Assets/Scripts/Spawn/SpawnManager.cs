using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] listSpawnerModel;
    private Spawner[] _listSpawner;
    private GameLevelManager _gameLevelManager;

    private void Start()
    {
        _gameLevelManager = GetComponentInParent<GameLevelManager>();
        _listSpawner = GetComponentsInChildren<Spawner>();
        
        for (int i = 0; i < _listSpawner.Length; i++)
        {
            SpawnModelWithType(_listSpawner[i],_gameLevelManager.enemySpawn[(int)_listSpawner[i].phaseSpawner-1].transform);
            _gameLevelManager.enemySpawn[(int)_listSpawner[i].phaseSpawner - 1].GetComponent<OpenGateNarrow>().isCheck = true;
        }
    }

    private void SpawnModelWithType(Spawner spawner, Transform parent = null)
    {
        Instantiate(listSpawnerModel[(int)spawner.typeSpawner], spawner.transform.position, Quaternion.identity,parent);
    }
}
