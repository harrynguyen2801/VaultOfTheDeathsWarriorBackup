using System;
using System.Collections;
using System.Collections.Generic;
using DissolveExample;
using UnityEngine;

public class SwordDissolve : MonoBehaviour
{

    List<Material> materials = new List<Material>();

    public bool isActive = false;
    private float duration = 2f;
    private float val;
    private float currentTime;
    void Start()
    {
        var renders = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renders.Length; i++)
        {
            materials.AddRange(renders[i].materials);
        }

        val = 1f;
        currentTime = 0f;
        duration = 5f;
    }
    
    public void Reset()
    {
        Start();
        SetValue(0);
    }
    
    public void SetValue(float value)
    {
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].SetFloat("_Dissolve", value);
        }
    }

    private void OnEnable()
    {
        Debug.Log(isActive + " is active");
        isActive = true;
    }

    void Update()
    {
        if (isActive)
        {
            while (currentTime < duration && val >= 0f)
            {
                currentTime += Time.deltaTime;
                val -= currentTime/duration;
                SetValue(val);
                Debug.Log(val + " is val");
                Debug.Log(currentTime/duration + " currentTime/duration");
                Debug.Log(currentTime + "  currentTime");
                Debug.Log(Time.deltaTime + " deltaTime");
            }
        }
    }
}
