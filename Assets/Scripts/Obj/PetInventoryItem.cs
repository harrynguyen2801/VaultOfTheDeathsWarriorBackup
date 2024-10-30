using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetInventoryItem : MonoBehaviour
{
    public Image itemBg;
    public Image imgName;
    public Image petImg;
    public Image petImgChoose;
    public Button choosePet;
    public TextMeshProUGUI petName;
    public int petId;
    public int petLv;
    private Tuple<string, int, int, int, int, string, int,Tuple<int>> dataPet;

    public void SetDataPet(int _petIdx, Tuple<string, int, int, int, int, string, int,Tuple<int>> data)
    {
        dataPet = data;
        var ePet = (EnumManager.EPet)Enum.ToObject(typeof(EnumManager.EPet), _petIdx);
        petImg.sprite = Resources.Load<Sprite>("PetAvatar/" + ePet + "/" + data.Item2);
        petName.text = data.Item1;
        petId = _petIdx;
        petLv = data.Item2;
        choosePet.onClick.AddListener(ChoosePet);
    }

    public void ShowItem(float time)
    {
        StartCoroutine(Show(time));
    }

    IEnumerator Show(float time)
    {
        yield return new WaitForSeconds(time);
        petImg.DOFade(1f, 0.2f);
        petName.DOFade(1f, 0.2f);
        itemBg.DOFade(.1f, 0.2f);
        imgName.DOFade(1f, 0.2f);
        petImgChoose.DOFade(1f, 0.2f);
    }

    public void ChoosePet()
    {
        NavContentPet navContentPet = GetComponentInParent<NavContentPet>();
        for (int i = 0; i < navContentPet.listPetItem.Count; i++)
        {
            navContentPet.listPetItem[i].GetComponent<PetInventoryItem>().petImgChoose.gameObject.SetActive(false);
        }
        petImgChoose.gameObject.SetActive(true);
        ActionManager.OnUpdateInformationPetTab?.Invoke(petId,petLv);
        ActionManager.OnUpdatePetInventoryModelView?.Invoke(petId, petLv);
    }
}
