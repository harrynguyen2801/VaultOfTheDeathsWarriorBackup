using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Typo;
using UnityEngine;
using UnityEngine.UI;

public class HelloTutorial : MonoBehaviour
{
    public GameObject mascot;
    public GameObject btnClose;
    
    public Image textContainer;
    public TypeWriterVfx tmpContent;

    private Animator _animMascot;
    private void Awake()
    {
        _animMascot = mascot?.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        ActionManager.OnUpdatenextStepTutorial += NextTutorial;
    }
    private void OnDisable()
    {
        ActionManager.OnUpdatenextStepTutorial -= NextTutorial;
    }

    public void ShowTutorialVert(int step)
    {
        btnClose.GetComponent<Button>().onClick.RemoveAllListeners();
        btnClose.GetComponent<Button>().onClick.AddListener(CloseFirstStepTutorial);
        StartCoroutine(ShowTutorialUp(step));
    }
    IEnumerator ShowTutorialUp(int step)
    {
        mascot.transform.localPosition = new Vector3(220f, -863f, 0f);
        _animMascot.Play("Fly");
        mascot.transform.DOMoveY(800, 1.25f);
        yield return new WaitForSeconds(1.75f);
        _animMascot.Play("Idle");
        textContainer.DOFade(1f, 1f);
        string text = DataManager.Instance.DataScriptTutorial[step];
        tmpContent.SetText(text);
    }

    public void ShowTutorialHori(int step)
    {
        btnClose.GetComponent<Button>().onClick.RemoveAllListeners();
        btnClose.GetComponent<Button>().onClick.AddListener(CloseStepTutorial);
        StartCoroutine(ShowTutorial(step));
    }

    IEnumerator ShowTutorial(int step)
    {
        mascot.transform.localPosition = new Vector3(-400f, 91f, 0f);
        mascot.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        mascot.transform.Rotate(0f, 0f, 0f);
        _animMascot.Play("Walk");
        mascot.transform.DOMoveX(500, 1f);
        yield return new WaitForSeconds(1.15f);
        _animMascot.Play("Idle");
        textContainer.DOFade(1f, 1f);
        string text = DataManager.Instance.DataScriptTutorial[step];
        tmpContent.SetText(text);
    }

    public void HideTutorialBtn()
    {
        StartCoroutine(HideTutorial());
    }

    IEnumerator HideTutorial()
    {
        mascot.transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
        _animMascot.Play("Walk");
        mascot.transform.DOMoveX(-500, 1f);
        yield return new WaitForSeconds(0.05f);
        _animMascot.Play("Idle");
        tmpContent.SetText("");
        textContainer.DOFade(0f, .5f);
    }
    
    public void NextTutorial(int step)
    {
        if (step != 0)
        {
            btnClose.SetActive(true);
            mascot.SetActive(true);
            ShowTutorialHori(step);
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.TutorialStep,step+1);
            VillageHomeScreen.Instance.modelNpcList[step-1].SetActive(true);
        }
        else
        {
            btnClose.SetActive(true);
            mascot.SetActive(true);
            ShowTutorialVert(step);
            DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.TutorialStep,step+1);
        }
    }

    public void ActiveTutorialPersonal()
    {
        CloseStepTutorial();
        mascot.SetActive(true);
        // DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.TutorialStep,DataManager.EDataPrefName.TutorialStep +1);
    }

    public void CloseStepTutorial()
    {
        btnClose.SetActive(false);
        HideTutorialBtn();
    }
    
    public void CloseFirstStepTutorial()
    {
        btnClose.SetActive(false);
        HideTutorialBtn();
        ActionManager.OnUpdatenextStepTutorial?.Invoke(DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialStep));;
    }
}
