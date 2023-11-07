using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

[CreateAssetMenu(menuName = "BasicAttacks/Base Basic Attack")]
public abstract class BasicAttack : ScriptableObject
{
    public GameObject Owner { get; protected set; }

    [SerializeField] private float attackRange;
    protected float Range { get { return attackRange; } }

    public virtual void Init(GameObject owner)
    {
        Owner = owner;
    }

    public virtual void Attack()
    {

    }

}
