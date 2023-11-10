using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Ability/BaseAcitveAbility")]
public class ActiveAbility : ScriptableObject
{
    [Header("UI")]
    [SerializeField] private Sprite abilityImageSprite;
    private AbilitySlot slot;
    [Space]
    [SerializeField] private float cooldown;

    public void Init(AbilitySlot abilitySlot)
    {
        slot = abilitySlot;
        slot.Init(abilityImageSprite);
    }

    public virtual void UseAbility()
    {
        slot.StartCooldown(cooldown);
    }

}
