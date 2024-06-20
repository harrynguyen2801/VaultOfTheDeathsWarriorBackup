using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScene;
    public Slider progressBar;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingScreenAsync(sceneName));
    }

    IEnumerator LoadingScreenAsync(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingScene.SetActive(true);
        while (!asyncOperation.isDone)
        {
            float progressVal = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            progressBar.value = progressVal;
            yield return null;
        }
        loadingScene.SetActive(false);
    }
}
