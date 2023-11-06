using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MinionType
{
    MELEE,
    RANGED
}

public class Minion : Character
{
    [Header("Minion Type")]
    [SerializeField] private MinionType minionType;

    [Header("Nav Mesh Detection")]
    [SerializeField] private float detectionRange;
    [SerializeField] private LayerMask layerMask;
    private float closestTargetDistance = 9999f;
    Collider[] targetsInRange;

    NavMeshAgent navAgent;
    Transform target;
    Stats minionStats;

    private bool isInitialized = false;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        BaseInitialize(this);
        minionStats = GetStats();

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = minionStats.TryGetStatValue(Stat.MOVEMENT_SPEED);

        if(minionType == MinionType.MELEE)
        {
            navAgent.stoppingDistance = 2.75f;
        }
        else if(minionType == MinionType.RANGED)
        {
            navAgent.stoppingDistance = 8f;
        }

        isInitialized = true;
    }

    private void Update()
    {
        if (isInitialized == false) return;

        animator.SetFloat("Velocity", navAgent.velocity.normalized.magnitude);
        
        MoveToTarget();
    }

    private void Attack()
    {

    }

    #region Navigation

    private void MoveToTarget()
    {
        if (target != null)
        {
            navAgent.SetDestination(target.position);
        }
        else
        {
            target = FindTarget();
        }
    }

    private Transform FindTarget()
    {
        Transform detectedTarget = null;
        targetsInRange = Physics.OverlapSphere(transform.position, detectionRange, layerMask);

        foreach (Collider targetCol in targetsInRange)
        {
            if (targetCol.transform == target) continue;

            float distance = Vector3.Distance(transform.position, targetCol.transform.position);
            if (distance < closestTargetDistance)
            {
                detectedTarget = targetCol.transform;
                closestTargetDistance = distance;
            }
        }

        Debug.Log($"{detectedTarget} Detected!");

        return detectedTarget;
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}
