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
    
    private float _sightRange = 4f
        , _attackRange = 5f;
    
    public bool playerInSightRange, enemyInAttackRange;
    public LayerMask whatIsPlayer,whatIsEnemy;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerTarget = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, whatIsPlayer);
        enemyInAttackRange = Physics.CheckSphere(transform.position, _attackRange, whatIsEnemy);

        if (playerInSightRange && enemyInAttackRange)
        {
            Debug.Log("companion Attack");
            _animator.SetTrigger(AnimationManager.Instance.animIDAttack);
        }
        else
        {
            _navMeshAgent.SetDestination(_playerTarget.position);
            _navMeshAgent.speed = 3f;
            _animator.SetFloat(AnimationManager.Instance.animIDWalk,_navMeshAgent.speed);
        }
    }
    
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,_attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,_sightRange);
    }
}
