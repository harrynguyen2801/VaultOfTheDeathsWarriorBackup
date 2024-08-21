using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameNpc;
    public TextMeshProUGUI mainConversation;
    public TextMeshProUGUI tmpBtnAccept;
    public TextMeshProUGUI tmpBtnEsc;

    public Image avatar;
    
    public Button btnEsc;
    public Button btnShop;

    public Image mainDialogue;
    public TextMeshProUGUI name;

    private void Start()
    {
        btnEsc.onClick.AddListener(EscapeDialogue);
    }

    public void SetDataDialogue(Tuple<string,string> dataNpc)
    {
        nameNpc.text = dataNpc.Item1;
        mainConversation.text = dataNpc.Item2;
        avatar.sprite = Resources.Load<Sprite>("AvatarNPC/" + dataNpc.Item1);
    }

    public void ActiveDialogue()
    {
        StartCoroutine(DialogueTween());
    }

    IEnumerator DialogueTween()
    {
        mainDialogue.DOFade(1, 0.5f);
        name.DOFade(1, 0.5f);
        nameNpc.DOFade(1, 0.5f);
        avatar.DOFade(1, 0.5f);
        mainConversation.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.35f);
        tmpBtnEsc.DOFade(1, 0.5f);
        tmpBtnAccept.DOFade(1, 0.5f);
        btnShop.GetComponent<Image>().DOFade(.85f, 0.5f);
        btnEsc.GetComponent<Image>().DOFade(.85f, 0.5f);
    }

    public void SetButtonFunc(int id)
    {
        switch (id)
        {
            case 1:
                btnShop.onClick.AddListener(VillageHomeScreen.Instance.OpenShopWeapon);
                tmpBtnAccept.text = "Go To Shop";
                break;
            case 2:
                btnShop.onClick.AddListener(VillageHomeScreen.Instance.OpenShopSkills);
                tmpBtnAccept.text = "Go To Shop";
                break;
            case 3:
                btnShop.onClick.AddListener(VillageHomeScreen.Instance.OpenShopGroceryShop);
                tmpBtnAccept.text = "Go To Shop";
                break;
            case 4:
                btnShop.onClick.AddListener(VillageHomeScreen.Instance.NextScene);
                tmpBtnAccept.text = "Start Travel";
                break;
        }
    }

    public void EscapeDialogue()
    {
        gameObject.SetActive(false);
    }
}
