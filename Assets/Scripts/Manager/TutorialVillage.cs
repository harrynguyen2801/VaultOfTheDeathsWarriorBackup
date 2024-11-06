using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] listObjTutorial;
    public GameObject mascotDialogue;

    private void OnEnable()
    {
        ActionManager.OnUpdatenextStepTutorial += NextTutorial;
    }

    private void OnDisable()
    {
        ActionManager.OnUpdatenextStepTutorial -= NextTutorial;
    }

    public void NextTutorial(int step)
    {
        Debug.Log("active step " + step);
        CloseStepTutorial();
        mascotDialogue.SetActive(true);
        DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.TutorialStep,step+1);
        listObjTutorial[step].SetActive(true);
        VillageHomeScreen.Instance.modelNpcList[step].SetActive(true);
    }

    public void ActiveTutorialPersonal()
    {
        CloseStepTutorial();
        mascotDialogue.SetActive(true);
        listObjTutorial[4].SetActive(true);
        DataManager.Instance.SaveDataPrefGame(DataManager.EDataPrefName.TutorialStep,4);
    }

    public void CloseStepTutorial()
    {
        foreach (var item in listObjTutorial)
        {
            item.SetActive(false);
        }
        mascotDialogue.SetActive(false);
    }
}
