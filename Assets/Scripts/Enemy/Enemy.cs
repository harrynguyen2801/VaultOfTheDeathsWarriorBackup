using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    //Enemy
    #region Component

    private NavMeshAgent _navMeshAgent;

    private Animator _animator;
    private Character _cc;

    #endregion
    
    #region Health

    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }

    #endregion

    #region EnemyAIVariables
    
    //
    private Transform _targetPlayer;
    public Transform targetPlayer => _targetPlayer;
    public LayerMask whatIsPlayer, whatIsGround;

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
            StartCoroutine(WaitForSeconds(3f));
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
        StartCoroutine(WaitForSeconds(0.3f));
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
        
    }
    private void Awake()
    {
        _cc = GetComponent<Character>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _targetPlayer = GameObject.FindWithTag("Player").transform;
        _navMeshAgent.speed = 2f;
        _animator = GetComponent<Animator>();
        _cc.SwitchStateTo(Character.CharacterState.Normal);
    }

    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = 100f;
        CurrentHealth = MaxHealth;
        _positionDefault = transform.position;
        walkPointCount = 0f;
    }

    
    public void CalculateMovementEnemy()
    {
        //check sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();




        // if (Vector3.Distance(_targetPlayer.position, transform.position) >= _navMeshAgent.stoppingDistance)
        // {
        //     _navMeshAgent.SetDestination(_targetPlayer.position);
        //     _animator.SetFloat(GameManager.Instance.animIDWalk,_navMeshAgent.speed);
        //     // Debug.Log(Vector3.Distance(_targetPlayer.position, transform.position));
        // }
        // else
        // {
        //     _navMeshAgent.SetDestination(transform.position);
        //     _animator.SetFloat(GameManager.Instance.animIDWalk, 0f);
        //     StartCoroutine(WaitForSeconds(0.3f));
        //     _cc.SwitchStateTo(Character.CharacterState.Attacking);
        // }
    }

    IEnumerator WaitForSeconds(float sec)
    {
        yield return new WaitForSeconds(sec);
    }
    
    public void ApplyDamage(float dmg, Vector3 posAttack = new Vector3())
    {
        CurrentHealth -= dmg;
        Debug.Log("enemy apply damage" + CurrentHealth);
    }
    
    public void RotateToTarget()
    {
        if (_cc.CurrentState != Character.CharacterState.Dead)
        {
            transform.LookAt(_targetPlayer, Vector3.up);
        }
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

    public void Die()
    {
        
    }
}
