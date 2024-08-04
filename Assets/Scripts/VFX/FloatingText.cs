using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloatingText : MonoBehaviour
{
    private float _timeDestroy = 2f;

    private Vector3 _offset = new Vector3(0f, 2f, 0f);
    private Vector3 _randomIntensity = new Vector3(0.75f, 0.3f, 0.3f);
    private Camera _cameraToLookAt;
    void Start()
    {
        _cameraToLookAt = Camera.main;
        transform.localPosition += _offset;
        transform.localPosition += new Vector3(Random.Range(-_randomIntensity.x, _randomIntensity.x),
            Random.Range(-_randomIntensity.y, _randomIntensity.y),Random.Range(-_randomIntensity.z, _randomIntensity.z));
        Destroy(gameObject,_timeDestroy);
    }

    private void LateUpdate()
    {
        transform.LookAt(_cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(_cameraToLookAt.transform.forward);
    }
}
