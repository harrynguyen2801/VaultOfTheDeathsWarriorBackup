using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDiedUI : MonoBehaviour
{
   public Image bgYouDied;
   public TextMeshProUGUI textYouDied;

   IEnumerator  YouDiedActive()
   {
      bgYouDied.DOFade(1f, .5f);
      textYouDied.DOFade(1f, .5f);
      yield return new WaitForSeconds(2f);
      bgYouDied.DOFade(0f, .5f);
      textYouDied.DOFade(0f, .5f);
      yield return new WaitForSeconds(1f);
      MainSceneManager.Instance.endingScreen.LoseGame();
      gameObject.SetActive(false);
   }

   private void Start()
   {
      StartCoroutine(YouDiedActive());
   }
}
