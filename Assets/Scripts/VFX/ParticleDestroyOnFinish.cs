using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyOnFinish : MonoBehaviour
{
    private ParticleSystem part;

    void OnEnable()
    {
        part = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (part.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
