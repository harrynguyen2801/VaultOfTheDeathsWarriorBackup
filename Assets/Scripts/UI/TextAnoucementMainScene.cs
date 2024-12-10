using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextAnoucementMainScene : MonoBehaviour
{
    private float[] _posY = new float[]{
        200f,500f,800f,
    };

    public TextMeshProUGUI content;
    private Vector3 _startPos;
    private void OnEnable()
    {
        ActionManager.OnUpdateAnoucementText += OnAnoucement;
    }
    
    private void OnDisable()
    {
        ActionManager.OnUpdateAnoucementText -= OnAnoucement;
    }

    private void Awake()
    {
        _startPos = content.transform.position;
    }

    public void OnAnoucement(string text)
    {
        content.transform.position = new Vector3(_startPos.x,_posY[DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.PosAnoucement)],_startPos.z);
        content.text = text;
        content.DOFade(1f, .5f);
        content.transform.DOMoveY(content.transform.position.y + 50f,.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(.3f, () =>
            {
                content.DOFade(0f, .5f);
                content.transform.DOMoveY(content.transform.position.y + 50f, .5f);
            });
        });
    }
}
