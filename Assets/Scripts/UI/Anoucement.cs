using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anoucement : MonoBehaviour
{
    public GameObject popup;

    public void ActiveAnoucement()
    {
        LeanTween.alpha(popup.GetComponent<RectTransform>(), 1f, .35f);
        LeanTween.scale(popup, new Vector3(1f, 1f, 1f), .15f).setDelay(.15f);
    }

    public void CloseAnoucement()
    {
        popup.transform.localScale = new Vector3(1f,0f,1f);
        gameObject.SetActive(false);
    }
}
