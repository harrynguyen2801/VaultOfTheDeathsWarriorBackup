using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipSceneIntro : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject introScene;
    public GameObject buttonSkip;
    public GameObject camera;

    private void Start()
    {
        StartCoroutine(waitForSeconds());
    }

    IEnumerator waitForSeconds()
    {
        yield return new WaitForSeconds(5f);
        buttonSkip.SetActive(true);
    }

    public void SkipTutorial()
    {
        LoadingScreen.Instance.LoadScreen(startScreen,introScene);
        camera.SetActive(true);
        gameObject.SetActive(false);
    }
}
