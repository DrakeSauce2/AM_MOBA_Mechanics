using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public enum MinionType
{
    MELEE,
    RANGED
}

[RequireComponent(typeof(NavMeshAgent))]
public class Minion : Character
{
    UIManager _UIManager;

    [Header("Minion Type")]
    [SerializeField] private MinionType minionType;

    [Header("Health")]
    [SerializeField] private Transform healthAttachPoint;
    ValueGauge healthGauge;

    [Header("Nav Mesh Detection")]
    [SerializeField] private float detectionRange;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask lookMask;
    private float closestTargetDistance = 9999f;
    Collider[] targetsInRange;

    NavMeshAgent navAgent;
    Transform target;
    Stats minionStats;

    private bool isInitialized = false;

    private void Awake()
    {
        Init();
        _UIManager = UIManager.Instance;

        healthGauge = CreateValueGauge(FindAnyObjectByType<Canvas>().GetComponent<RectTransform>());
        healthGauge.Initialize(GetStats().TryGetStatValue(Stat.HEALTH), GetStats().TryGetStatValue(Stat.MAXHEALTH), Color.red);
        UIAttachComponent attachComponent = healthGauge.gameObject.AddComponent<UIAttachComponent>();
        attachComponent.Init(healthAttachPoint);

        minionStats.onValueChanged -= UpdateHealthBar;
        minionStats.onValueChanged += UpdateHealthBar;
    }

    private ValueGauge CreateValueGauge(RectTransform transform)
    {
        GameObject valueGuageInstance = UIManager.Instance.CreateValueGauge(transform);
        return valueGuageInstance.GetComponent<ValueGauge>();
    }

    private void UpdateHealthBar(Stat statChanged, float value)
    {
        if (statChanged == Stat.HEALTH)
        {
            healthGauge.SetValue(value);
            return;
        }
    }

    public void Init()
    {
        BaseInitialize(this);

        minionStats = GetStats();

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = minionStats.TryGetStatValue(Stat.MOVEMENT_SPEED);

        if (minionType == MinionType.MELEE)
        {
            navAgent.stoppingDistance = 2.75f;
        }
        else if (minionType == MinionType.RANGED)
        {
            navAgent.stoppingDistance = 12f;
        }

        isInitialized = true;
    }

    private void Update()
    {
        if (isInitialized == false) return;

        
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

        if(!detectedTarget) return null;

        if (Physics.Raycast(transform.position, detectedTarget.position, detectionRange, lookMask))
        {
            Debug.Log($"{detectedTarget} Detected!");
            return detectedTarget;
        }
       
        return null;
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}