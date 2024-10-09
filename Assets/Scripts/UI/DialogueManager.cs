using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Typo;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameNpc;
    public TextMeshProUGUI mainConversation;
    public TextMeshProUGUI tmpBtnAccept;
    public TextMeshProUGUI tmpBtnEsc;

    public Image avatar;
    public Image nameImg;

    public Button btnEsc;
    public Button btnShop;

    public Image mainDialogue;

    private void Start()
    {
        btnEsc.onClick.AddListener(ExitDialogue);
    }

    public void SetDataDialogue(Tuple<string,string> dataNpc)
    {
        nameNpc.text = dataNpc.Item1;
        mainConversation.gameObject.GetComponent<TypeWriterVfx>().SetText(dataNpc.Item2);
        avatar.sprite = Resources.Load<Sprite>("AvatarNPC/" + dataNpc.Item1);
        // AddressableUltilities.Instance.LoadAndSetSprite("AvatarNPC/" + dataNpc.Item1,avatar);
    }

    public void ActiveDialogue()
    {
        StartCoroutine(DialogueTween());
    }

    IEnumerator DialogueTween()
    {
        mainDialogue.DOFade(1, 0.5f);
        nameImg.DOFade(1, 0.5f);
        nameNpc.DOFade(1, 0.5f);
        avatar.DOFade(1, 0.5f);
        mainConversation.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.2f);
        tmpBtnEsc.DOFade(1, 0.45f);
        tmpBtnAccept.DOFade(1, 0.45f);
        btnShop.GetComponent<Image>().DOFade(.9f, 0.45f);
        btnEsc.GetComponent<Image>().DOFade(.9f, 0.45f);
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
                btnShop.onClick.AddListener(VillageHomeScreen.Instance.OpenLevelScreen);
                tmpBtnAccept.text = "Start Travel";
                break;
        }
    }

    public void ExitDialogue()
    {
        avatar.DOFade(0, 0.1f);
        gameObject.SetActive(false);
    }
}
