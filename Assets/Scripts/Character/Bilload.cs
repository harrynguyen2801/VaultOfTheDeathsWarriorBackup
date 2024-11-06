using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilload : MonoBehaviour
{

    public GameObject cam;
    private void Awake()
    {
        cam = GameObject.FindWithTag("MainCamera");
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
    
}
    