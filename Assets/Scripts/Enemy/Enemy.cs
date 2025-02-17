using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    public enum ClassEnemy
    {
        Normal,
        Boss,
    }
    
    [FormerlySerializedAs("typeEnemy")] public DataManager.EEnemyType typeEEnemy;
    public CharacterController characterController;

    public ClassEnemy classEnemy;
    
    #region Component

    private NavMeshAgent _navMeshAgent;

    private Animator _animator;
    private Character _cc;
    #endregion
    
    #region Health
    public float MaxHealth { get; set; }
    private float CurrentHealth { get; set; }
    
    public GameObject healthBar;

    #endregion

    #region EnemyAIVariables
    
    public GameObject[] listItem;

    private Transform _targetPlayer;
    public Transform TargetPlayer => _targetPlayer;
    public LayerMask whatIsPlayer, whatIsGround;
    public Vector3 PosPlayer => _posPlayer;
    private Vector3 _posPlayer;
    private float _attackAnimationDuration;
    public int countAttackCombo;

    //Patrolling
    public Vector3 walkPoint;
    public bool _walkPointSet;
    public float walkPointRange;
    public float walkPointCount;
    public Vector3 _positionDefault;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    #endregion

    private int _xp;
    List<Material> materials = new List<Material>();
    public int countBeingHit = 0;
    public int hitToDefend = 0;
    public bool _isInvincible = false;
    public float _invincibleDuration = 1.5f;

    public GameObject floatingText;
    public bool phase2Attack = false;
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,sightRange);
    }

    #region EnemyAI

    private void Patrolling()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet)
        {
            _navMeshAgent.SetDestination(walkPoint);
            _navMeshAgent.speed = 1f;
            _animator.SetFloat(AnimationManager.Instance.animIDWalk,_navMeshAgent.speed);
        }

        //walkPoint Reached
        Vector3 distanceWalkPoint = transform.position - walkPoint;
        if (distanceWalkPoint.magnitude < 1f)
        {
            _walkPointSet = false;
        }
    }
    private void ChasePlayer()
    {
        _navMeshAgent.SetDestination(_targetPlayer.position);
        _navMeshAgent.speed = 2.5f;
        _animator.SetFloat(AnimationManager.Instance.animIDWalk,_navMeshAgent.speed);
    }
    private void AttackPlayer()
    {
        countAttackCombo++;
        Debug.Log("countAttackCombo: " + countAttackCombo);
        //make sure enemy don't move
        _navMeshAgent.SetDestination(transform.position);
        transform.LookAt(_targetPlayer);
        _animator.SetFloat(AnimationManager.Instance.animIDWalk, 0f);
        _posPlayer = TargetPlayer.position;
        _cc.SwitchStateTo(Character.CharacterState.Attacking);
    }
    
    private void SearchWalkPoint()
    {
        //random point in range
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        if (walkPointCount <= 2)
        {
            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            _walkPointSet = true;
            walkPointCount++;
        }
        else
        {
            walkPointCount = 0;
            walkPoint = _positionDefault;
            _walkPointSet = true;
        }
        _animator.SetFloat(AnimationManager.Instance.animIDWalk, 1f);
    }
    
    public void EnemyAttackCombo()
    { 
        _attackAnimationDuration = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Flame Attack" 
            && _attackAnimationDuration > 0.75f && _attackAnimationDuration < 0.9f)
        {
            _cc.SwitchStateTo(Character.CharacterState.Attacking);
            CalculateMovementEnemy();
        }
    }
    
    #endregion

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        _cc = GetComponent<Character>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _targetPlayer = GameObject.FindWithTag("Player").transform;
        _navMeshAgent.speed = 2f;
        _animator = GetComponent<Animator>();
        _cc.SwitchStateTo(Character.CharacterState.Normal);
        countAttackCombo = 0;
        MaxHealth = DataManager.Instance.DataEnemy.Single(h => h.Key == typeEEnemy).Value.Item1 * DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.LevelOpen);
        CurrentHealth = MaxHealth;
        _xp = (int)Mathf.Floor(DataManager.Instance.DataEnemy.Single(h => h.Key == typeEEnemy).Value.Item2 + 50 * DataManager.Instance.GetDataPrefPlayer(DataManager.EDataPlayerEquip.LevelPlayer)/3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        _positionDefault = transform.position;
        walkPointCount = 0f;
        
        
        var renders = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renders.Length; i++)
        {
            materials.AddRange(renders[i].materials);
        }
    }

    #region EnemyMove

    public void CalculateMovementEnemy()
    {
        //check sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (classEnemy == ClassEnemy.Boss)
        {
            if (!playerInSightRange && !playerInAttackRange)
            {
                _animator.SetFloat(AnimationManager.Instance.animIDWalk,0f);
                _animator.SetTrigger(AnimationManager.Instance.animIDIdle);
                _navMeshAgent.SetDestination(transform.position);
            }
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
        else
        {
            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
    }
    
    
    public void LookAtTarget()
    {
        Quaternion newRotation = Quaternion.LookRotation(_targetPlayer.position - transform.position);
        transform.rotation = newRotation;
    }

    #endregion


    #region EnemyDamageable
    
    public void ApplyDamage(float dmg, Vector3 posAttack = new Vector3())
    {
        CurrentHealth -= dmg;
        if (floatingText)
        {
            ShowFloatingText(dmg);
        }
        if (CurrentHealth <= 0)
        {
            _cc.SwitchStateTo(Character.CharacterState.Dead);
            return;
        }
        GetComponentInChildren<EnemyVFXManager>().PlayerBeingHitVFX(posAttack);
        countBeingHit++;
        hitToDefend++;
        _cc.SwitchStateTo(Character.CharacterState.BeingHit);
        Debug.Log("enemy apply damage" + CurrentHealth);
    }

    private void ShowFloatingText(float dmg)
    {
        var textFloating = Instantiate(floatingText, transform.position, quaternion.identity, transform);
        textFloating.GetComponent<TextMeshPro>().text = dmg.ToString();
    }
    
    public void EnemyBeingHit()
    {
        if (classEnemy == ClassEnemy.Boss && countBeingHit >= 3)
        {
            countBeingHit = 0;
            _cc.SwitchStateTo(Character.CharacterState.Defend);
        }

        if (classEnemy == ClassEnemy.Boss && CurrentHealth <= MaxHealth/2f && !phase2Attack && gameObject.name == "pbDragonRed")
        {
            phase2Attack = true;
            Debug.Log("Dragon fly");
            _cc.SwitchStateTo(Character.CharacterState.Fly);
            ActionManager.OnBossChangePhase?.Invoke(2);
            MainSceneManager.Instance.ZoomOutCamera();
        }
    }

    public float GetMaxHealth()
    {
        return MaxHealth;
    }
    
    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }
    
    public void InvicibleEnemy()
    {
        _isInvincible = true;
        characterController.detectCollisions = false;
        StartCoroutine(DelayCancelInvincible());
    }
    
    IEnumerator DelayCancelInvincible()
    {
        yield return new WaitForSeconds(_invincibleDuration);
        characterController.detectCollisions = true;
        _isInvincible = false;
    }
    
    public void InvicibleEnemyNoTime()
    {
        _isInvincible = true;
        characterController.detectCollisions = false;
        characterController.enabled = false;
    }

    public void DisableInvincibility()
    {
        _isInvincible = false;
        characterController.detectCollisions = true;
        characterController.enabled = true;
    }
    
    public void ActiveHealthBar()
    {
        healthBar.SetActive(true);
    }
    
    public void DeActiveHealthBar()
    {
        healthBar.SetActive(false);
    }
    
    public void DestroyEnemy()
    {
        ActionManager.OnUpdateXpAndLevelPlayer?.Invoke(_xp);
        StartCoroutine(DissolveDeath());
        if (classEnemy == ClassEnemy.Normal)
        {
            DropSingle();
        }
        else
        {
            DropAll();
        }
    }
    
    IEnumerator DissolveDeath()
    {
        float duration = 1f;
        float time = 0f;
        float dissolveValue = 0f;

        while (time < duration)
        {
            time += Time.deltaTime / duration;
            dissolveValue = Mathf.Lerp(0f, 1f, time / duration);
            for (int i = 0; i < materials.Count; i++)
            {
                materials[i].SetFloat("_Dissolve", dissolveValue);
            }
            yield return null;
        }
        Destroy(gameObject);
    }
    #endregion


    #region EnemyDrop

    private void DropSingle()
    {
        int rand = Random.Range(0, listItem.Length);
        Instantiate(listItem[rand], new Vector3(transform.position.x,transform.position.y + .7f,transform.position.z), Quaternion.identity);
    }
    
    private void DropAll()
    {
        for (int i = 0; i < listItem.Length; i++)
        {
            Instantiate(listItem[i], new Vector3(transform.position.x+1.5f,transform.position.y + .7f,transform.position.z + 1.5f), Quaternion.identity);
        }
    }

    #endregion


    public void SetAnimatorClip(int _anima)
    {
        _animator.SetTrigger(_anima);
    }

    private void OnEnable()
    {
        ActionManager.OnUpdateUIPlayerDie += DeActiveHealthBar;
    }

    private void OnDisable()
    {
        ActionManager.OnUpdateUIPlayerDie -= DeActiveHealthBar;
    }

    private void Update()
    {
        if (playerInSightRange)
        {
            ActiveHealthBar();
        }
        else
        {
            DeActiveHealthBar();
        }
    }
    
    public void EnemySfxDefend()
    {
        SoundManager.Instance.PlaySfxEnemy(EnumManager.ESfxSoundPlayer.Defend);
    }
    
    public void EnemySfxHit()
    {
        SoundManager.Instance.PlaySfxEnemy(EnumManager.ESfxSoundPlayer.Hit);
    }

    public void StopBgmBoss()
    {
        SoundManager.Instance.StopSoundBossBgm();
    }
}
