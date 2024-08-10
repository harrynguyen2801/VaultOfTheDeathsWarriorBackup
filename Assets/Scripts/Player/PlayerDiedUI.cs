using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiedUI : MonoBehaviour
{
   public GameObject bgYouDied;
   public GameObject textYouDied;

   IEnumerator  YouDiedActive()
   {
      LeanTween.alpha(bgYouDied.GetComponent<RectTransform>(), 1f, .5f);
      LeanTween.alpha(textYouDied.GetComponent<RectTransform>(), 1f, .5f);
      yield return new WaitForSeconds(2f);
      LeanTween.alpha(bgYouDied.GetComponent<RectTransform>(), 0f, .5f);
      LeanTween.alpha(textYouDied.GetComponent<RectTransform>(), 0f, .5f);
      yield return new WaitForSeconds(1f);
      MainSceneManager.Instance.endingScreen.LoseGame();
      gameObject.SetActive(false);
   }

   private void Start()
   {
      StartCoroutine(YouDiedActive());
   }
}
