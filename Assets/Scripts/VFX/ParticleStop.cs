using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStop : MonoBehaviour
{
    // Start is called before the first frame update
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
            part.gameObject.SetActive(false);
        }
    }
}
