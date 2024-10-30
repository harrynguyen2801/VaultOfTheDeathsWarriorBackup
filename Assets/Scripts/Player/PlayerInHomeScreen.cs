using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerInHomeScreen : MonoBehaviour
{
    #region Components

    private Character _cc;
    private CharacterController _characterController;
    public Character_Input input;
    private Animator _animator;
    #endregion
    
    #region MovementVariables

    private float _moveSpeed;
    private readonly float _runSpeed = 2.5f;
    private readonly float _sprintSpeed = 4f;
    private readonly float _jumpHeight = 2.2f;
    
    private float _attackAnimationDuration;
    private readonly float _gravity = -9.81f;
    
    private Vector3 _verticalVelocity;
    private Vector3 _movementVelocity;
    private Vector3 _impactOnPlayer;
    
    private float _fallTimeoutDelta;
    private float _jumpTimeoutDelta;
    private bool _jumpEnd;

    public float jumpTimeout = 0.3f;
    public float fallTimeout = 0.15f;
    #endregion
    
    #region VisualPlayerVariables

    public GameObject visualMale;
    public GameObject visualFemale;
    
    public Avatar maleAvatar;
    public Avatar feMaleAvatar;
    
    //Set sex player: true is male, false is female
    public bool isMale;

    #endregion
    

    #region OtherVariables

    public bool isNpcInRange;
    public GameObject pressFTointeract;

    private bool _hasAnimator;

    private bool _isInvincible;
    private float _invincibleDuration = 1f;

    private Tuple<string, string, int, int, int, string, int, Tuple<int>> _weaponEquip;
    private int _damageWeapon;
    public int damageWeapon => _damageWeapon;

    public LayerMask whatIsNpc;

    private float _npcSightRange = 2f;
    
    #endregion
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        input = GetComponent<Character_Input>();
        _animator = GetComponent<Animator>();
        _cc = GetComponent<Character>();
    }
    
    private void Start()
    {
        int idWeapon = DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.WeaponId);
        if (idWeapon == 0)
        {
            _weaponEquip = DataManager.Instance.GetWeaponByID(1);
        }
        else
        {
            _weaponEquip = DataManager.Instance.GetWeaponByID(idWeapon);
        }
        _hasAnimator = TryGetComponent(out _animator);

        _jumpTimeoutDelta = jumpTimeout;
        _fallTimeoutDelta = fallTimeout;
        _isInvincible = false; // tesst
        _jumpEnd = true;
        
        if (DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.PlayerSex) == 1)
        {
            visualFemale.SetActive(true);
            _animator.avatar = feMaleAvatar;
        }
        else
        {
            visualMale.SetActive(true);
            _animator.avatar = maleAvatar;
        }
    }
    
    public void CalculateMovementPlayer()
    {
        // if (input.roll && _characterController.isGrounded && _jumpEnd)
        // {
        //     _animator.SetTrigger(AnimationManager.Instance.animIDRoll);
        //     return;
        // }
        
        _moveSpeed = input.sprint ? _sprintSpeed : _runSpeed;
        
        if (input.move == Vector2.zero) _moveSpeed = 0.0f;
        if (_characterController.isGrounded && _verticalVelocity.y < 0) _verticalVelocity.y = 0;
        
        float inputMagnitude = input.move.magnitude;
        
        _movementVelocity = new Vector3(input.move.x,0f,input.move.y);
        _movementVelocity.Normalize();
        _movementVelocity = Quaternion.Euler(0, -45f, 0) * _movementVelocity;
        _movementVelocity *= _moveSpeed * Time.deltaTime;
        
        if (_movementVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_movementVelocity);
        }

        _animator.SetFloat(AnimationManager.Instance.animIDSpeed,_moveSpeed);
        _animator.SetFloat(AnimationManager.Instance.animIDMotionSpeed,inputMagnitude);
        
        if (_hasAnimator)
        {
            _animator.SetBool(AnimationManager.Instance.animIDGrounded, _characterController.isGrounded);
        }
        
        // move the player
        _characterController.Move(_movementVelocity + _verticalVelocity * Time.deltaTime);
        _movementVelocity = Vector3.zero;
        
        input.ClearCache();
    }
    
    public void JumpAndGravity()
    {
        if (_characterController.isGrounded)
        {
            _fallTimeoutDelta = fallTimeout;
            
            if (_hasAnimator)
            {
                _animator.SetBool(AnimationManager.Instance.animIDJump, false);
                _animator.SetBool(AnimationManager.Instance.animIDFall, false);
            }
                        
            //stop dropping infinite when grounded
            if (_verticalVelocity.y < 0.0f)
            {
                _verticalVelocity.y = -2f;
            }

            if (input.jump && _jumpTimeoutDelta <= 0.0f)
            {
                _verticalVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravity);
                _animator.SetBool(AnimationManager.Instance.animIDJump, true);
                _jumpEnd = false;
            }
            
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            _jumpTimeoutDelta = jumpTimeout;
            
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                if (_hasAnimator)
                {
                    _animator.SetBool(AnimationManager.Instance.animIDFall, true);
                }
            }
            
            _verticalVelocity.y += _gravity * Time.deltaTime;
            input.jump = false;
        }
        
        
    }
    
    public void JumpToNormal()
    {
        _jumpEnd = true;
        Debug.Log(_jumpEnd + " jump end");
    }

    private void FixedUpdate()      
    {
        JumpAndGravity();
        CalculateMovementPlayer();
    }

    private void Update()
    {
        isNpcInRange = Physics.CheckSphere(transform.position, _npcSightRange, whatIsNpc);
        if (isNpcInRange)
        {
            pressFTointeract.GetComponent<TextMeshProUGUI>().DOFade(1f, 0.5f);
        }
        else
        {
            pressFTointeract.GetComponent<TextMeshProUGUI>().DOFade(0f, 0.5f);
        }
        
        if (input.openShop)
        {
            pressFTointeract.GetComponent<TextMeshProUGUI>().DOFade(0f, 0.5f);
            Collider[] colliders = Physics.OverlapSphere(transform.position, _npcSightRange, whatIsNpc);
            foreach (var obj in colliders)
            {
                if (obj.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.NpcInteract();
                }
            }
        }
    }
    
    public void RollAnimationEnds()
    {
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,_npcSightRange);
    }
}
