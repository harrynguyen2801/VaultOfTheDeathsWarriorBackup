using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    public enum ClassEnemy
    {
        Normal,
        Boss,
    }
    public DataManager.EnemyType typeEnemy;
    public CharacterController characterController;

    public ClassEnemy classEnemy;
    //Enemy
    #region Component

    private NavMeshAgent _navMeshAgent;

    private Animator _animator;
    private Character _cc;

    #endregion
    
    #region Health

    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public GameObject healthBar;

    #endregion

    #region EnemyAIVariables
    
    //
    public GameObject[] listItem;

    private Transform _targetPlayer;
    public Transform targetPlayer => _targetPlayer;
    public LayerMask whatIsPlayer, whatIsGround;
    public Vector3 PosPlayer => _posPlayer;
    private Vector3 _posPlayer;
    private float _attackAnimationDuration;
    private int _countAttackCombo;

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

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,sightRange);
    }


    private void Patrolling()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet)
        { 
            _navMeshAgent.SetDestination(walkPoint);
            _navMeshAgent.speed = 1f;
            _animator.SetFloat(GameManager.Instance.animIDWalk,_navMeshAgent.speed);
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
        _animator.SetFloat(GameManager.Instance.animIDWalk,_navMeshAgent.speed);
    }
    private void AttackPlayer()
    {
        //make sure enemy don't move
        _navMeshAgent.SetDestination(transform.position);
        transform.LookAt(_targetPlayer);
        _animator.SetFloat(GameManager.Instance.animIDWalk, 0f);
        _posPlayer = targetPlayer.position;
        _cc.SwitchStateTo(Character.CharacterState.Attacking);
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
        
    }
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        _cc = GetComponent<Character>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _targetPlayer = GameObject.FindWithTag("Player").transform;
        _navMeshAgent.speed = 2f;
        _animator = GetComponent<Animator>();
        _cc.SwitchStateTo(Character.CharacterState.Normal);
        _countAttackCombo = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = DataManager.Instance.DataHealthEnemy.Single(h => h.Key == typeEnemy).Value;
        CurrentHealth = MaxHealth;
        _positionDefault = transform.position;
        walkPointCount = 0f;
    }

    
    public void CalculateMovementEnemy()
    {
        //check sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (classEnemy == ClassEnemy.Boss)
        {
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
    
    public void ApplyDamage(float dmg, Vector3 posAttack = new Vector3())
    {
        CurrentHealth -= dmg;
        if (CurrentHealth <= 0)
        {
            _cc.SwitchStateTo(Character.CharacterState.Dead);
            return;
        }
        _cc.SwitchStateTo(Character.CharacterState.BeingHit);
        Debug.Log("enemy apply damage" + CurrentHealth);
    }

    public void LookAtTarget()
    {
        Quaternion newRotation = Quaternion.LookRotation(_targetPlayer.position - transform.position);
        transform.rotation = newRotation;
    }

    public void SetAnimatorClip(int _anima)
    {
        _animator.SetTrigger(_anima);
    }

    public void DestroyEnemy()
    {
        if (classEnemy == ClassEnemy.Normal)
        {
            DropSingle();
        }
        else
        {
            DropAll();
        }

        Destroy(gameObject);
    }

    private void DropSingle()
    {
        int rand = Random.Range(0, listItem.Length);
        Instantiate(listItem[rand], new Vector3(transform.position.x,transform.position.y + .7f,transform.position.z), Quaternion.identity);
    }
    
    private void DropAll()
    {
        for (int i = 0; i < listItem.Length; i++)
        {
            Instantiate(listItem[i], new Vector3(transform.position.x+1.5f,transform.position.y + .7f,transform.position.z+1.5f), Quaternion.identity);
        }
    }
    
    public void ActiveHealthBar()
    {
        healthBar.SetActive(true);
    }
    
    public void DeActiveHealthBar()
    {
        healthBar.SetActive(false);
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
}
