using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float _speed = 80f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f,_speed * Time.deltaTime, 0f),Space.World);
    }
}
