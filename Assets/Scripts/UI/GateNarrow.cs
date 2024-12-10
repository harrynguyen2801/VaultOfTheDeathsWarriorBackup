using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GateNarrow : MonoBehaviour
{
    public GameObject[] gates1;
    public GameObject[] gates2;
    public GameObject[] gates3;

    private void OnEnable()
    {
        ActionManager.OnOpenGateNarrow += OnOpenGate;
    }
    
    private void OnDisable()
    {
        ActionManager.OnOpenGateNarrow -= OnOpenGate;
    }

    public void OnOpenGate(int gateOpen)
    {
        switch (gateOpen)
        {
            case 1:
                OpenAllGate(gates1);
                break;
            case 2:
                OpenAllGate(gates2);
                break;
            case 3:
                OpenAllGate(gates3);
                break;
        }
    }

    private void OpenAllGate(GameObject[] gates)
    {
        for (int i = 0; i < gates.Length; i++)
        {
            gates[i].transform.DOMoveY(-1f, 2.5f);
        }
    }
}
