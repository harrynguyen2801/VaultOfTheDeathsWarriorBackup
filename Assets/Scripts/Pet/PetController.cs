using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetController : MonoBehaviour
{
    private Transform _playerTarget;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private DamageCasterForPet _damageCaster;

    [SerializeField]
    private float _sightRangePlayer = 4f
        , _sightRangeEnemy = 6f, _attackRange = 3f;
    
    public bool playerInSightRange, enemyInAttackRange, enemyInSightRange;
    public LayerMask whatIsPlayer,whatIsEnemy;
    
    private float _attackRate = 2f;
    private float _nextAttack = 1f; 
    private void Awake()
    {
        _damageCaster = GetComponentInChildren<DamageCasterForPet>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerTarget = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, _sightRangePlayer, whatIsPlayer);
        enemyInAttackRange = Physics.CheckSphere(transform.position, _attackRange, whatIsEnemy);
        enemyInSightRange = Physics.CheckSphere(transform.position, _sightRangeEnemy, whatIsEnemy);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _sightRangeEnemy,whatIsEnemy);

        _nextAttack -= Time.deltaTime;
        while (_nextAttack <= 0 )
        {
            _nextAttack = _attackRate;
            if (playerInSightRange && enemyInAttackRange)
            {
                _navMeshAgent.SetDestination(transform.position);
                transform.LookAt(hitColliders[0].transform);
                _animator.SetFloat(AnimationManager.Instance.animIDWalk, 0f);
                Debug.Log("companion Attack");
                _animator.SetTrigger(AnimationManager.Instance.animIDAttack);
            }
        }

        if (playerInSightRange && enemyInSightRange)
        {
            _navMeshAgent.SetDestination(hitColliders[0].gameObject.transform.position);
            _navMeshAgent.speed = 3f;
            _animator.SetFloat(AnimationManager.Instance.animIDWalk,_navMeshAgent.speed);
        }
        else
        {
            if (Vector3.Distance(transform.position, _playerTarget.position) <= _navMeshAgent.stoppingDistance)
            {
                _animator.SetFloat(AnimationManager.Instance.animIDWalk,0f);
                // _animator.SetTrigger(AnimationManager.Instance.animIDIdle);
                _navMeshAgent.SetDestination(transform.position);
            }
            else
            {
                transform.LookAt(_playerTarget.transform);
                _navMeshAgent.SetDestination(_playerTarget.position);
                _navMeshAgent.speed = 3f;
                _animator.SetFloat(AnimationManager.Instance.animIDWalk,_navMeshAgent.speed);
            }
        }
    }
    
    public void EnableDamageCaster()
    {
        _damageCaster.EnableDamageCaster();
    }
    
    public void DisableDamageCaster()
    {
        _damageCaster.DisableDamageCaster();
    }
    
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,_attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,_sightRangePlayer);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,_sightRangeEnemy);
    }
    
}
