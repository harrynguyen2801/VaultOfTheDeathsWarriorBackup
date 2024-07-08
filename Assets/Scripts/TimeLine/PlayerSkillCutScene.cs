using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillCutScene : MonoBehaviour
{
    public GameObject cutScene;
    private void OnEnable()
    {
        cutScene.SetActive(false);
    }
}
