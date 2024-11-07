using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Typo;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEquip : MonoBehaviour
{
    public GameObject mascot;
    public GameObject btnClose;
    
    public Image textContainer;
    public TypeWriterVfx tmpContent;

    private Animator _animMascot;
    public GameObject arrow1;
    public GameObject arrow2;
    
    private List<string> mascotScript = new List<string>()
    {
        "Click the buy button here to buy yourself a weapon",
        "You have successfully purchased return to the village to continue exploring.",
    };
    private void Awake()
    {
        _animMascot = mascot?.GetComponent<Animator>();
    }

    private void Start()
    {
        ShowTutorialHori(0);
    }

    public void ShowTutorialHori(int step)
    {
        btnClose.GetComponent<Button>().onClick.RemoveAllListeners();
        btnClose.GetComponent<Button>().onClick.AddListener(CloseStepTutorial);
        StartCoroutine(ShowTutorial(step));
        if (step == 0)
        {
            arrow1.SetActive(true);
        }
        if (step == 1)
        {
            arrow2.SetActive(true);
        }
    }

    IEnumerator ShowTutorial(int step)
    {
        mascot.transform.localPosition = new Vector3(2053f, -448f, 0f);
        mascot.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        _animMascot.Play("Walk");
        mascot.transform.DOMoveX(1300, 1f);
        yield return new WaitForSeconds(1.15f);
        _animMascot.Play("Idle");
        textContainer.DOFade(1f, 1f);
        tmpContent.SetText(mascotScript[step]);
    }

    public void HideTutorialBtn()
    {
        StartCoroutine(HideTutorial());
    }

    IEnumerator HideTutorial()
    {
        _animMascot.Play("Walk");
        mascot.transform.DOMoveX(-800, 1.25f);
        yield return new WaitForSeconds(0.05f);
        _animMascot.Play("Idle");
        tmpContent.SetText("");
        textContainer.DOFade(0f, .5f);
        yield return new WaitForSeconds(1.25f);
        mascot.transform.localPosition = new Vector3(2053f, -448f, 0f);
    }

    public void CloseStepTutorial()
    {
        btnClose.SetActive(false);
        HideTutorialBtn();
    }
}
