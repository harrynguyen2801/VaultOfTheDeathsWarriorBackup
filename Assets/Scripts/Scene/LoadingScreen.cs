using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScene;
    public Slider progressBar;

    public Image bg;
    public TextMeshProUGUI textTitle;
    public TextMeshProUGUI textContent;

    private float _timeDelay = 1.5f;
    
    private static LoadingScreen _instance;
    public static LoadingScreen Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingSceneAsync(sceneName));
    }
    
    public void LoadScreen(GameObject screen1, GameObject screen2)
    {
        StartCoroutine(LoadingScreenAsync(screen1,screen2));
    }
    
    public void LoadScreen(GameObject screen1)
    {
        StartCoroutine(LoadingScreenAsync(screen1));
    }

    public void SetDataLoadingScreen()
    {
        int bgID = Random.Range(1, 4);
        int textID = Random.Range(1, 16);
        textTitle.text = DataManager.Instance.WeaponsDatas.ElementAt(textID).Value.Item1;
        textContent.text = DataManager.Instance.WeaponsDatas.ElementAt(textID).Value.Item6;
        bg.sprite = Resources.Load<Sprite>("LoadingBG/" + bgID);
        // AddressableUltilities.Instance.LoadAndSetSprite("LoadingBG/" + bgID,bg);
    }

    IEnumerator LoadingSceneAsync(string sceneName)
    {
        SetDataLoadingScreen();
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

    IEnumerator LoadingScreenAsync(GameObject screen1, GameObject screen2)
    {
        loadingScene.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        float progressVal = 0.3f;
        progressBar.value = progressVal;
        yield return new WaitForSeconds(0.3f);
        progressVal = 0.6f;
        progressBar.value = progressVal;
        yield return new WaitForSeconds(0.7f);
        progressVal = 0.9f;
        progressBar.value = progressVal;
        yield return new WaitForSeconds(0.3f);
        loadingScene.SetActive(false);
        screen1.SetActive(true);
        screen2.SetActive(false);
    }
    
    IEnumerator LoadingScreenAsync(GameObject screen1)
    {
        loadingScene.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        float progressVal = 0.3f;
        progressBar.value = progressVal;
        yield return new WaitForSeconds(0.3f);
        progressVal = 0.6f;
        progressBar.value = progressVal;
        yield return new WaitForSeconds(0.7f);
        progressVal = 0.9f;
        progressBar.value = progressVal;
        yield return new WaitForSeconds(0.3f);
        loadingScene.SetActive(false);
        screen1.SetActive(true);
    }
}
