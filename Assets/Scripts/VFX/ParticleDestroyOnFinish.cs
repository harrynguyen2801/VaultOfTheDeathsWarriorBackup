using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyOnFinish : MonoBehaviour
{
    // Start is called before the first frame update
    private ParticleSystem part;
    private Collider _damageCaster;

    private void Awake()
    {
        _damageCaster = GetComponent<Collider>();
        _damageCaster.enabled = false;
    }

    void OnEnable()
    {
        part = GetComponent<ParticleSystem>();
        StartCoroutine(waitForVfxPlay());
    }

    IEnumerator waitForVfxPlay()
    {
        yield return new WaitForSeconds(1f);
        _damageCaster.enabled = true;
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
