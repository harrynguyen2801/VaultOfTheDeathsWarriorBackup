using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    private static GameManager _instance;
    public GameObject profile;

    public GameObject[] levelList;
    public GameObject player;
    public GameObject enemySpawn;
    public EndScreenManager endingScreen;

    public bool winOrLose;

    #region Animation Ids Variables

    public int animIDSpeed;
    public int animIDGrounded;
    public int animIDJump;
    public int animIDFall;
    public int animIDMotionSpeed;
    public int animIDWalk;
    public int animIDDead;
    public int animIDBeingHit;
    public int animIDAttack;
    public int animIDRoll;
    public int animIDDefend;
    public int canAttack;

    #endregion

    private void Awake()
    {
        AssignAnimationIDs();
        ShowCurrentLevel();
        _instance = this;
        profile.SetActive(true);
    }

    private void ShowCurrentLevel()
    {
        int level = DataManager.Instance.GetDataInt(DataManager.DataPrefName.Level);
        Debug.Log(level + " level");
        if (PlayerPrefs.HasKey("Level"))
        {
            levelList[level-1].gameObject.SetActive(true);
            player.GetComponent<Transform>().position = levelList[level - 1].GetComponent<GameLevelManager>().playerStartPosition.position;
        }
        else
        {
            levelList[0].gameObject.SetActive(true);
            player.GetComponent<Transform>().position = levelList[0].GetComponent<GameLevelManager>().playerStartPosition.position;
            DataManager.Instance.SaveData(DataManager.DataPrefName.Level,1);
        }
    }

    public void ShowNextLevel(int level)
    {
        int levelSave = DataManager.Instance.GetDataInt(DataManager.DataPrefName.Level);
        levelList[levelSave-1].gameObject.SetActive(false);
        levelList[level-1].gameObject.SetActive(true);
        player.GetComponent<Transform>().position = levelList[level-1].GetComponent<GameLevelManager>().playerStartPosition.position;
        StartCoroutine(waitSecond(3f));
        player.GetComponent<Player>().AppearPlayerInGame();
    }
    
    IEnumerator waitSecond(float sec)
    {
        yield return new WaitForSeconds(sec);
    }

    private void AssignAnimationIDs()
    {
        animIDSpeed = Animator.StringToHash("Speed");
        animIDGrounded = Animator.StringToHash("Ground");
        animIDJump = Animator.StringToHash("Jump");
        animIDFall = Animator.StringToHash("Fall");
        animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        animIDWalk = Animator.StringToHash("Walk");
        animIDDead = Animator.StringToHash("Dead");
        animIDBeingHit = Animator.StringToHash("BeingHit");
        animIDAttack = Animator.StringToHash("Attack");
        animIDRoll = Animator.StringToHash("Roll");
        animIDDefend = Animator.StringToHash("Defend");
    }
}
