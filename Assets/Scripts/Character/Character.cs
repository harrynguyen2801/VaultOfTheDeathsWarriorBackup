using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Character : MonoBehaviour
{
    private Player _player;
    private Enemy _enemy;
    private Animator _animator;

    public bool isPlayer = true;
    
    //DamageCaster
    private DamageCaster _damageCaster;

    #region Finite State Machine Variables

    public enum CharacterState
    {
        Normal, Attacking, Dead, BeingHit, Slide, Spawn, Sprint, Roll, Jump, Defend, Skill, Fly
    }
    public CharacterState CurrentState;

    #endregion

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        _damageCaster = GetComponentInChildren<DamageCaster>();
    }

    private void FixedUpdate()
    {
        switch (CurrentState)
        {
            case CharacterState.Normal:
                if (isPlayer)
                {
                    _player.JumpAndGravity();
                    _player.CalculateMovementPlayer();
                }
                else
                {
                    _enemy.CalculateMovementEnemy();
                }
                break;
            case CharacterState.Attacking:
                if (isPlayer)
                {
                   _player.SlidePlayerAttack();
                   _player.PlayerAttackCombo();
                }
                else
                {
                    if (_enemy.classEnemy == Enemy.ClassEnemy.Boss)
                    {
                        _enemy.EnemyAttackCombo();
                    }
                }
                break;
            case CharacterState.Slide:
                break;
            case CharacterState.BeingHit:
                if (isPlayer)
                {               
                    _player.PlayerBeingHit();
                }
                else
                {
                    _enemy.EnemyBeingHit();
                }
                break;
            case CharacterState.Dead:
                break;
            case CharacterState.Fly:
                break;
        }

        if (isPlayer)
        {
            _player.PlayerMove();
            _player.CheckEnemyInRangeSkill();
            // _player.ManaRecoveryAuto();
        }
    }

    public void SwitchStateTo(CharacterState newState)
    {
        if (isPlayer)
        {
            _player.input.ClearCache();
        }
        switch (CurrentState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attacking:
                if (_damageCaster != null)
                {
                    DisableDamageCaster();
                }
                break;
            case CharacterState.Sprint:
                break;
            case CharacterState.Slide:
                break;
            case CharacterState.Spawn:
                break;
            case CharacterState.Dead:
                break;
            case CharacterState.Roll:
                break;
            case CharacterState.Jump:
                break;
            case CharacterState.Defend:
                if (_damageCaster != null)
                {
                    DisableDamageCaster();
                }
                break;
            case CharacterState.Skill:
                break;
            case CharacterState.Fly:
                break;
        }
        
        switch (newState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attacking:
                if (isPlayer)
                {
                    _animator.SetTrigger(AnimationManager.Instance.animIDAttack);
                    _player.attackStartTime = Time.time;
                }
                else
                {
                    if (_enemy.countAttackCombo >= 2)
                    {
                        _animator.SetTrigger(AnimationManager.Instance.animIDAttack2);
                    }
                    if (_enemy.countAttackCombo >= 3)
                    {
                        _animator.SetTrigger(AnimationManager.Instance.animIDAttack2);
                        _enemy.countAttackCombo = 0;
                    }
                    else
                    {
                        _animator.SetTrigger(AnimationManager.Instance.animIDAttack);
                    }
                    _enemy.LookAtTarget();
                }
                break;
            case CharacterState.Sprint:
                break;
            case CharacterState.Slide:
                break;
            case CharacterState.BeingHit:
                _animator.SetTrigger(AnimationManager.Instance.animIDBeingHit);
                if (isPlayer)
                {
                    _player.InviciblePlayer(1f);
                }
                break;
            case CharacterState.Spawn:
                break;
            case CharacterState.Dead:
                if (isPlayer)
                {
                    _player.Die();
                }
                else
                {
                    _enemy.characterController.enabled = false;
                    _enemy.sightRange = 0f;
                    _enemy.attackRange = 0f;
                }
                _animator.SetTrigger(AnimationManager.Instance.animIDDead);
                break;
            case CharacterState.Roll:
                _animator.SetTrigger(AnimationManager.Instance.animIDRoll);
                break;
            case CharacterState.Jump:
                break;
            case CharacterState.Defend:
                _animator.SetTrigger(AnimationManager.Instance.animIDDefend);
                _enemy.InvicibleEnemy();
                break;
            case CharacterState.Skill:
                break;
            case CharacterState.Fly:
                _enemy.InvicibleEnemyNoTime();
                _animator.SetTrigger(AnimationManager.Instance.animIDTakeOff);
                break;
        }

        CurrentState = newState;
    }
    
    public void AttackAnimationEnds()
    {
        SwitchStateTo(CharacterState.Normal);
    }

    public void BeingHitAnimationEnds()
    {
        SwitchStateTo(CharacterState.Normal);
    }
    
    public void RollAnimationEnds()
    {
        SwitchStateTo(CharacterState.Normal);
    }
    
    public void DefendAnimationEnds()
    {
        SwitchStateTo(CharacterState.Normal);
    }
    
    public void SkillAnimationEnds()
    {
        SwitchStateTo(CharacterState.Normal);
    }

    public void EnableDamageCaster()
    {
        _damageCaster.EnableDamageCaster();
    }
    
    public void DisableDamageCaster()
    {
        _damageCaster.DisableDamageCaster();
    }
}
