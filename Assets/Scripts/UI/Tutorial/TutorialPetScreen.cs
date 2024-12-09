using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Typo;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPetScreen : MonoBehaviour
{
    public GameObject mascot;
    public Image textContainer;
    public TypeWriterVfx tmpContent;
    private Animator _animMascot;
    public GameObject arrow1;
    public GameObject arrow2;
    public GameObject arrow3;

    private List<string> mascotScript = new List<string>()
    {
        "Welcome to the pet store, choose a pet to accompany you.",
        "Click the buy button here to buy yourself a pet",
        "You have successfully purchased return to the village to continue exploring.",
    };
    private void Awake()
    {
        _animMascot = mascot?.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ActionManager.OnUpdateNextStepPetScreenTutorial += ShowTutorialHori;
    }

    private void OnDisable()
    {
        ActionManager.OnUpdateNextStepPetScreenTutorial -= ShowTutorialHori;
    }

    public void ShowTutorialHori(int step)
    {
        StartCoroutine(ShowTutorial(step));
    }

    IEnumerator ShowTutorial(int step)
    {
        mascot.transform.localPosition = new Vector3(-300f, 91f, 0f);
        mascot.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        mascot.transform.Rotate(0f, 0f, 0f);
        _animMascot.Play("Walk");
        mascot.transform.DOMoveX(500, 1f);
        yield return new WaitForSeconds(1.15f);
        _animMascot.Play("Idle");
        textContainer.DOFade(1f, 1f);
        tmpContent.SetText(mascotScript[step]);
        if (step == 1)
        {
            arrow2.SetActive(true);
        }
        if (step == 2)
        {
            arrow3.SetActive(true);
        }
    }

    public void HideTutorialBtn()
    {
        StartCoroutine(HideTutorial());
    }

    IEnumerator HideTutorial()
    {
        mascot.transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
        _animMascot.Play("Walk");
        mascot.transform.DOMoveX(-600, 1f);
        yield return new WaitForSeconds(0.05f);
        _animMascot.Play("Idle");
        tmpContent.SetText("");
        textContainer.DOFade(0f, .5f);
        arrow1.SetActive(false);
        arrow2.SetActive(false);
        arrow3.SetActive(false);
    }
}
