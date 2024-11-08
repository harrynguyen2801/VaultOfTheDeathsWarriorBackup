using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayer : MonoBehaviour
{
    private Transform _npcTransform = new RectTransform();
    private bool _activeArrSearch = false;
    private float _speedRotaion = 3f;
    private void OnEnable()
    {
        ActionManager.OnUpdatenextStepTutorial += OnUpdateDirectionLookNpc;
    }

    private void OnDisable()
    {
        ActionManager.OnUpdatenextStepTutorial -= OnUpdateDirectionLookNpc;
    }

    private void OnUpdateDirectionLookNpc(int npcId)
    {
        _activeArrSearch = true;
        _npcTransform = VillageHomeScreen.Instance.modelNpcList[npcId].transform;
    }

    private void Update()
    {
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.Tutorial) == 0 && _activeArrSearch)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(VillageHomeScreen.Instance.playerModelEquipManager.transform.position - _npcTransform.position), _speedRotaion * Time.deltaTime);
        }
    }
}
