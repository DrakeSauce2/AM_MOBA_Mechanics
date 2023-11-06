using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Ability/BaseAcitveAbility")]
public class ActiveAbility : ScriptableObject
{
    [Header("UI")]
    [SerializeField] private Sprite abilityImageSprite;
    [Space]
    [SerializeField] private float cooldown;

    public void Init(AbilitySlot abilitySlot)
    {

    }

    public virtual void UseAbility()
    {

    }

}
