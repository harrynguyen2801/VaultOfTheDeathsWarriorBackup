using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IDamageable
{

    #region Health

    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }

    #endregion

    #region Components

    private Character _cc;
    private CharacterController _characterController;
    public Character_Input input;
    private Animator _animator;
    private VFXPlayerController _vfxPlayerController;
    private DamageCaster _damageCaster;
    private PlayerSkillsBarController _playerSkillsBarController;

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

    #region AimAttackVariables

    public LayerMask isEnemy;
    private bool _enemyInSightRange;
    public float sightRange;
    #endregion

    #region VisualPlayerVariables

    public GameObject visualMale;
    public GameObject visualFemale;
    
    public Avatar maleAvatar;
    public Avatar feMaleAvatar;
    
    //Set sex player: true is male, false is female
    public bool isMale;

    #endregion

    #region PlayerSlideVariables
    
    public float attackStartTime;
    public float attackSlideDuraton = 0.25f;
    public float attackSlideSpeed = 0.05f;
    
    #endregion

    #region OtherVariables

    private bool _hasAnimator;

    private bool _isInvincible;
    private float _invincibleDuration = 5f;

    public GameObject ultimateCutScene;
    public GameObject guardSkillCutScene;
    public GameObject magicSkillCutScene;
    public GameObject swordSkillCutScene;

    public GameObject playerDiedUI;

    private Tuple<string, string, int, int, int, string, int, Tuple<int>> _weaponEquip;
    private int _damageWeapon;
    public int damageWeapon => _damageWeapon;

    public Vector3 enemyInRangeSkill;
    private float sightRangeSkill = 10f;
    #endregion


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        input = GetComponent<Character_Input>();
        _animator = GetComponent<Animator>();
        _vfxPlayerController = GetComponent<VFXPlayerController>();
        _playerSkillsBarController = GetComponentInChildren<PlayerSkillsBarController>();
        _cc = GetComponent<Character>();
    }

    private void Start()
    {
        _characterController.enabled = false;
        AppearPlayerInGame();
        int idWeapon = DataManager.Instance.GetDataInt(DataManager.EDataPrefName.WeaponId);
        _weaponEquip = DataManager.Instance.GetWeaponByID(idWeapon);
        MaxHealth = _weaponEquip.Item4;
        CurrentHealth = MaxHealth;
        _damageWeapon = _weaponEquip.Item3;
        
        _damageCaster = GetComponentInChildren<DamageCaster>();
        _hasAnimator = TryGetComponent(out _animator);

        _jumpTimeoutDelta = jumpTimeout;
        _fallTimeoutDelta = fallTimeout;
        _isInvincible = false; // tesst
        _jumpEnd = true;
    }
    
    public void AppearPlayerInGame()
    {
        StartCoroutine(AppearPlayer());
    }
    
    public void DissapearPlayerInGame()
    {
        StartCoroutine(DissapearPlayer());
    }

    IEnumerator AppearPlayer()
    {
        _vfxPlayerController.PlayVfxTrailsUp();
        yield return new WaitForSeconds(1f);
        AODPlayer(true);
        _characterController.enabled = true;
    }
    
    IEnumerator DissapearPlayer()
    {
        _vfxPlayerController.PlayVfxTrailsDown();
        yield return new WaitForSeconds(0.5f);
        AODPlayer(false);
        _characterController.enabled = false;
    }

    private void AODPlayer(bool aod)
    {
        if (DataManager.Instance.GetDataInt(DataManager.EDataPrefName.PlayerSex) == 1)
        {
            visualFemale.SetActive(aod);
            _animator.avatar = feMaleAvatar;
        }
        else
        {
            visualMale.SetActive(aod);
            _animator.avatar = maleAvatar;
        }
    }
    
    public void CalculateMovementPlayer()
    {
        _enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, isEnemy);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sightRange,isEnemy);

        if (input.guard && _playerSkillsBarController.finishCDGuard && _characterController.isGrounded && _jumpEnd)
        {
            guardSkillCutScene.SetActive(true);
            guardSkillCutScene.GetComponent<PlayableDirector>().Play();
            _cc.SwitchStateTo(Character.CharacterState.Skill);
            InviciblePlayer();
            return;
        }
        
        if (input.sword && _playerSkillsBarController.finishCDSword && _characterController.isGrounded && _jumpEnd)
        {
            swordSkillCutScene.SetActive(true);
            swordSkillCutScene.GetComponent<PlayableDirector>().Play();
            _cc.SwitchStateTo(Character.CharacterState.Skill);
            return;
        }
        
        if (input.magic && _playerSkillsBarController.finishCDMagic && _characterController.isGrounded && _jumpEnd)
        {
            magicSkillCutScene.SetActive(true);
            magicSkillCutScene.GetComponent<PlayableDirector>().Play();
            _cc.SwitchStateTo(Character.CharacterState.Skill);
            return;
        }
        else
        {
            input.ClearSkillInput();
        }
        
        if (input.attack && _characterController.isGrounded && _jumpEnd)
        {
            _cc.SwitchStateTo(Character.CharacterState.Attacking);
            if (_enemyInSightRange)
            {
                transform.LookAt(hitColliders[0].transform);
            }
            return;
        }
        
        if (input.roll && _characterController.isGrounded && _jumpEnd)
        {
            _cc.SwitchStateTo(Character.CharacterState.Roll);
            return;
        }
        
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
    }

    public void PlayerMove()
    {
        if (_cc.CurrentState != Character.CharacterState.Dead)
        {
            // move the player
            _characterController.Move(_movementVelocity + _verticalVelocity * Time.deltaTime);
            _movementVelocity = Vector3.zero;
        } 
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

    public void CheckEnemyInRangeSkill()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,sightRangeSkill, isEnemy);
        if (colliders.Length != 0)
        {
            enemyInRangeSkill = colliders[0].transform.position;
        }
        else
        {
            enemyInRangeSkill = new Vector3(transform.position.x + 1,transform.position.y,transform.position.z);
        }
    }

    public void ApplyDamage(float dmg, Vector3 posAttack = new Vector3())
    {
        CurrentHealth -= dmg;
        Debug.Log("dmg player: " + dmg);
        if (CurrentHealth <= 0)
        {
            Debug.Log("death");
            _cc.SwitchStateTo(Character.CharacterState.Dead);
        }
        
        _cc.SwitchStateTo(Character.CharacterState.BeingHit);
        AddImpact(posAttack,5f);

        float perHealth = CurrentHealth / MaxHealth;
        ProfileManager.Instance.SetHealthAndMana(perHealth,1f);
    }

    public void AddHealth(float healVal)
    {
        CurrentHealth += healVal;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        float perHealth = CurrentHealth / MaxHealth;
        ProfileManager.Instance.SetHealthAndMana(perHealth,1f);
    }

    public void SlidePlayerAttack()
    {                    
        if (Time.deltaTime < attackSlideDuraton + attackStartTime)
        {
            float timePassed = Time.time - attackStartTime;
            float lerpTime = timePassed / attackSlideDuraton;
            _movementVelocity = Vector3.Lerp(transform.forward * attackSlideSpeed, Vector3.zero, lerpTime);
        }
    }

    public void PlayerAttackCombo()
    {
        if (input.attack && _characterController.isGrounded && _jumpEnd)
        {
            _attackAnimationDuration = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Combo03" 
                && _attackAnimationDuration > 0.5f && _attackAnimationDuration < 0.75f)
            {
                _cc.SwitchStateTo(Character.CharacterState.Attacking);
                input.attack = false;
                CalculateMovementPlayer();
            }
            else
            {
                input.attack = false;
                _cc.SwitchStateTo(Character.CharacterState.Normal);
            }
        }
    }

    public void PlayerBeingHit()
    {
        if (_impactOnPlayer.magnitude > 0.2f)
        {
            _movementVelocity = _impactOnPlayer * Time.deltaTime;
        }
        _impactOnPlayer = Vector3.Lerp(_impactOnPlayer, Vector3.zero, Time.deltaTime * 5);
    }

    public void JumpToNormal()
    {
        _jumpEnd = true;
        Debug.Log(_jumpEnd + " jump end");
    }

    public void StopBladeAnimation()
    {
        GetComponent<VFXPlayerController>().StopBlade();
    }
    
    public void AddImpact(Vector3 attackerPos, float force)
    {
        Vector3 impactDir = transform.position - attackerPos;
        impactDir.Normalize();
        impactDir.y = 0;
        _impactOnPlayer = impactDir * force;
    }

    public void InviciblePlayer()
    {
        _isInvincible = true;
        _characterController.detectCollisions = false;
        StartCoroutine(DelayCancelInvincible());
    }
    
    IEnumerator DelayCancelInvincible()
    {
        yield return new WaitForSeconds(_invincibleDuration);
        _characterController.detectCollisions = true;
        _isInvincible = false;
    }

    public void Die()
    {
        _characterController.enabled = false;
    }

    public void LoadScreenLose()
    {
        // StartCoroutine(DelayToLoadScreenLose());
        playerDiedUI.SetActive(true);
    }

    IEnumerator DelayToLoadScreenLose()
    {
        yield return new WaitForSeconds(1f);
        MainSceneManager.Instance.profile.SetActive(false);
        MainSceneManager.Instance.endingScreen.LoseGame();
    }
    

    public void PickUpItem(DropItem item)
    {
        switch (item.type)
        {
            case DropItem.ItemType.Coin:
                var coin = DataManager.Instance.GetDataInt(DataManager.EDataPrefName.Coin);
                Debug.Log(coin);
                coin += 100;
                DataManager.Instance.SaveData(DataManager.EDataPrefName.Coin,coin);
                Debug.Log(coin);
                break;
            case DropItem.ItemType.HealOrb:
                AddHealth(30);
                _vfxPlayerController.PlayerVfxHealing();
                break;
        }
    }
    
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,sightRange);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,sightRangeSkill);
    }

    public void CancelTriggerAttack()
    {
        _animator.ResetTrigger(AnimationManager.Instance.animIDAttack);
    }
}
