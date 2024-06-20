using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTween : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Tween();
    }

    public void Tween()
    {
        LeanTween.alpha(gameObject, 1f, 0.5f).setDelay(2f);
    }
}
