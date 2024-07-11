using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineVillage : MonoBehaviour
{
    [SerializeField] private Image _blackScreenImage;
    [SerializeField] private Text _blackScreenText1;
    [SerializeField] private Text _blackScreenText2;
    
    [SerializeField] private float _blackScreenDuration = 3f;
    [SerializeField] private float _fadingDuration = 1.5f;
    private bool _screenTimerIsActive = true;
    void Start()
    {
        _blackScreenImage.gameObject.SetActive(true);
        _blackScreenText1.gameObject.SetActive(true);
        _blackScreenText2.gameObject.SetActive(true);
    }

    void Update()
    {
        if (_screenTimerIsActive)
        {
            _blackScreenDuration -= Time.deltaTime;
            if (_blackScreenDuration < 0)
            {
                _screenTimerIsActive = false;
                _blackScreenImage.CrossFadeAlpha(0, _fadingDuration, false);
                _blackScreenText1.CrossFadeAlpha(0, _fadingDuration, false);
                _blackScreenText2.CrossFadeAlpha(0, _fadingDuration, false);
            }
        }
    }
}
