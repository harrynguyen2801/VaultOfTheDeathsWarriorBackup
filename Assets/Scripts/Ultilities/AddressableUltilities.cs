using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class AddressableUltilities : MonoBehaviour
{
    private static AddressableUltilities _instance;
    public static AddressableUltilities Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    public void LoadAndSetSprite(string path , Image imgTarget)
    { 
        Debug.Log(path);
        imgTarget.sprite = null;
        var handle = Addressables.LoadAsset<Sprite>(path);
        handle.Completed += task =>
        {
            imgTarget.sprite = handle.Result;
        };
        Addressables.Release(handle);
    }
}
