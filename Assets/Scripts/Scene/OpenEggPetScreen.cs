using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class OpenEggPetScreen : MonoBehaviour
{
    public GameObject timelineOpenEggPet;
    public CanvasGroup cgBtnOpenEggPet;
    public Pet3DModelContainer pet3DModelContainer;
    private void OnEnable()
    {
        StartCoroutine(ActiveBtnOpenEggPet());
    }

    public void OpenEggPet()
    {
        timelineOpenEggPet.SetActive(true);
    }

    IEnumerator ActiveBtnOpenEggPet()
    {
        yield return new WaitForSeconds(5f);
        float timer = 0;
        float duration = 0.5f;
        while (timer < duration)
        {
            cgBtnOpenEggPet.alpha = Mathf.Lerp(cgBtnOpenEggPet.alpha, 1, timer/duration);
            timer += Time.deltaTime;
            yield return null;
        }
        cgBtnOpenEggPet.interactable = true;
    }

    public void CloseEggPetScene()
    {
        VillageHomeScreen.Instance.DeactiveOpenEgg();
        gameObject.SetActive(false);
    }
}
