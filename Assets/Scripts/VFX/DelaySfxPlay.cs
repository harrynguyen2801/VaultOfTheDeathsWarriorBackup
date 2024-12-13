using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySfxPlay : MonoBehaviour
{
    public float delayTimeSfx;
    
    public AudioSource audioSource;
    public AudioClip clip;

    private void Start()
    {
        PlaySfx();
    }

    public void PlaySfx()
    {
        StartCoroutine(DelaySfx());
    }
    IEnumerator DelaySfx()
    {
        yield return new WaitForSeconds(delayTimeSfx);
        audioSource.PlayOneShot(clip);
    }
}
