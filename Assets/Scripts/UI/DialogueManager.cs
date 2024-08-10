using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameNpc;
    public TextMeshProUGUI mainConversation;
    
    public Image avatar;
    
    public Button btnEsc;
    public Button btnShop;
    
    public GameObject mainDialogue;
    public GameObject name;

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
        LeanTween.alpha(mainDialogue.GetComponent<RectTransform>(), 1f, 0.5f);
        LeanTween.alpha(name.GetComponent<RectTransform>(), 1f, 0.5f);
        LeanTween.alpha(nameNpc.GetComponent<RectTransform>(), 1f, 0.5f);
        LeanTween.alpha(avatar.GetComponent<RectTransform>(), 1f, 0.5f);
        LeanTween.alpha(mainConversation.GetComponent<RectTransform>(), 1f, 0.5f);
        yield return new WaitForSeconds(0.75f);
        LeanTween.alpha(btnShop.GetComponent<RectTransform>(), .75f, 0.35f);
        LeanTween.alpha(btnEsc.GetComponent<RectTransform>(), .75f, 0.25f);
    }

    public void SetButtonFunc(int id)
    {
        switch (id)
        {
            case 1:
                btnShop.onClick.AddListener(VillageHomeScreen.Instance.OpenShopWeapon);
                break;
            case 2:
                btnShop.onClick.AddListener(VillageHomeScreen.Instance.OpenShopSkills);
                break;
            case 3:
                btnShop.onClick.AddListener(VillageHomeScreen.Instance.OpenShopGroceryShop);
                break;
        }
    }

    public void EscapeDialogue()
    {
        gameObject.SetActive(false);
    }
}
