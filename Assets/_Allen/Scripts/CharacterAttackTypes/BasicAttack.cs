using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BasicAttacks/BaseBasicAttack")]
public class BasicAttack : ScriptableObject
{
    


}

[Serializable]
public class AttackSequence
{
    
}

[Serializable]
public class Attack
{
    [SerializeField] private AnimationClip attackAnimation;
    [SerializeField] private float attackTime;
    [Space]
    [SerializeField] private float nextAttackWindow;
}
