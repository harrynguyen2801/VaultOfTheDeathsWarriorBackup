using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Anoucement : MonoBehaviour
{
    public Image popup;

    public void ActiveAnoucement()
    {
        popup.DOFade(1f, .35f);
        DOTween.Sequence().SetDelay(.15f).Append( popup.transform.DOMove(new Vector3(1f, 1f, 1f), .15f));
    }

    public void CloseAnoucement()
    {
        popup.transform.localScale = new Vector3(1f,0f,1f);
        gameObject.SetActive(false);
    }
}
