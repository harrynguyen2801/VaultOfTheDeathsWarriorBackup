using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Typo;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPersonal : MonoBehaviour
{
    public GameObject mascot;
    public GameObject btnClose;
    
    public Image textContainer;
    public TypeWriterVfx tmpContent;

    private Animator _animMascot;

    public string textMascot;
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
        // btnClose.GetComponent<Button>().onClick.RemoveAllListeners();
        // btnClose.GetComponent<Button>().onClick.AddListener(CloseStepTutorial);
        StartCoroutine(ShowTutorial(step));
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
        tmpContent.SetText(textMascot);
        yield return new WaitForSeconds(3f);
        HideTutorialBtn();
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
