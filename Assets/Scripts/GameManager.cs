using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    private static GameManager _instance;

    public GameObject[] levelList;
    public GameObject player;
    
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
    public int canAttack;

    #endregion

    private void Awake()
    {
        ShowCurrentLevel();
        _instance = this;
        AssignAnimationIDs();
    }

    private void ShowCurrentLevel()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            levelList[PlayerPrefs.GetInt("Level")-1].gameObject.SetActive(true);
            player.GetComponent<Transform>().position = levelList[PlayerPrefs.GetInt("Level") - 1].GetComponent<GameLevelManager>().playerStartPosition.position;
        }
        else
        {
            levelList[0].gameObject.SetActive(true);
            player.GetComponent<Transform>().position = levelList[0].GetComponent<GameLevelManager>().playerStartPosition.position;
            PlayerPrefs.SetInt("Level",1);
        }
    }

    public void ShowNextLevel(int level)
    {
        levelList[PlayerPrefs.GetInt("Level")-1].gameObject.SetActive(false);
        PlayerPrefs.SetInt("Level",level);
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
    }
}
