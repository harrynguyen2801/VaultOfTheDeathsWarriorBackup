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
    }
    public GatePhaseOpen phase;
    void Update()
    {
        if (transform.childCount <= 0)
        {
            ActionManager.OnOpenGateNarrow?.Invoke((int)phase);
            Debug.Log("Open gate phase " + phase);
            Destroy(gameObject);
        }   
    }
}
