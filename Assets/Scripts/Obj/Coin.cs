using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float _speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(transform.rotation.x,_speed * Time.deltaTime, transform.rotation.z));
    }
}
