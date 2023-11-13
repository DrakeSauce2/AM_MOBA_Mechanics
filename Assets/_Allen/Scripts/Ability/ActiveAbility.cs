using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static ActiveAbility;

public enum CastingState
{
    NONE,
    STARTCAST,
    CASTING
}

[CreateAssetMenu(menuName = "Ability/BaseAcitveAbility")]
public class ActiveAbility : ScriptableObject
{
    public GameObject Owner { get; private set; }

    [Header("Casting State")]
    [SerializeField] private CastingState castingState;

    [Header("UI")]
    [SerializeField] private Sprite abilityImageSprite;
    private AbilitySlot slot;
    [Space]
    [SerializeField] private float cooldown;

    [Header("Ability Outline")]
    [SerializeField] private GameObject abilityOutlinePrefab;
    private GameObject instancedAbilityOutline;
    [SerializeField] private Vector3 outlinePositionOffset;

    //

    public delegate void OnAbilityStart();
    public delegate void OnAbilityEnd();
    public OnAbilityStart onAbilityStart;   
    public OnAbilityEnd onAbilityEnd;

    //

    public void Init(GameObject owner, AbilitySlot abilitySlot)
    {
        Owner = owner;
        slot = abilitySlot;
        slot.Init(abilityImageSprite);

        instancedAbilityOutline = Instantiate(abilityOutlinePrefab, Owner.transform.position + outlinePositionOffset,
                                              Quaternion.identity, Owner.transform);

        instancedAbilityOutline.SetActive(false);
    }

    public void Cast(Animator characterAnimator, int abilityIndex)
    {
        if (slot.cooldown < cooldown) return;

        switch (castingState)
        {
            case CastingState.NONE:
                StartAbility();
                return;
            case CastingState.STARTCAST:
                characterAnimator.SetTrigger($"Ability{abilityIndex + 1}");
                UseAbility();
                return;
            case CastingState.CASTING:

                return;
        }
    }

    public void StopCast()
    {
        instancedAbilityOutline.SetActive(false);
        castingState = CastingState.NONE;
    }

    private void StartAbility()
    {
        
        castingState = CastingState.STARTCAST;
        instancedAbilityOutline.SetActive(true);
    }

    private void EndAbility()
    {
        onAbilityEnd?.Invoke();
    }

    protected virtual void UseAbility()
    {   
        onAbilityStart?.Invoke();  
        slot.StartCooldown(cooldown);
        instancedAbilityOutline.SetActive(true);
    }

}
