using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGateNarrow : MonoBehaviour
{
    public enum GatePhaseOpen
    {
        Phase1 = 1,
        Phase2 = 2,
        Phase3 = 3,
        Phase4 = 4,
    }
    public GatePhaseOpen phase;
    public bool isCheck;
    void Update()
    {
        if (!isCheck) return;
        if (transform.childCount > 0) return;
        ActionManager.OnOpenGateNarrow?.Invoke((int)phase);
        Debug.Log("Open gate phase " + phase);
        Destroy(gameObject);
    }
}
