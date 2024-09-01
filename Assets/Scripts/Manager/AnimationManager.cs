using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
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
    public int animIDAttack2;
    public int animIDAttack3;
    public int animIDRoll;
    public int animIDDefend;
    public int canAttack;

    #endregion
    
    public static AnimationManager Instance => _instance;
    private static AnimationManager _instance;
    private void Awake()
    {
        AssignAnimationIDs();
        _instance = this;
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
        animIDAttack2 = Animator.StringToHash("Attack2");
        animIDAttack3 = Animator.StringToHash("Attack3");
        animIDRoll = Animator.StringToHash("Roll");
        animIDDefend = Animator.StringToHash("Defend");
    }
}
