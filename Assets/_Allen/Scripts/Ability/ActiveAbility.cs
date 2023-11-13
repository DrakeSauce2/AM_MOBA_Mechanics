using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum CastingState
{
    STARTCAST,
    CASTED,
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

    public void Init(GameObject owner, AbilitySlot abilitySlot)
    {
        Owner = owner;
        slot = abilitySlot;
        slot.Init(abilityImageSprite);

        instancedAbilityOutline = Instantiate(abilityOutlinePrefab, Owner.transform.position + outlinePositionOffset,
                                              Quaternion.identity, Owner.transform);

        instancedAbilityOutline.SetActive(false);
    }

    public virtual void UseAbility()
    {      
        switch (castingState)
        {
            case CastingState.STARTCAST:
                instancedAbilityOutline.SetActive(true);
                castingState = CastingState.CASTED;
                return;
            case CastingState.CASTED:
                instancedAbilityOutline.SetActive(false);
                castingState = CastingState.CASTING;
                return;
            case CastingState.CASTING:
                slot.StartCooldown(cooldown);
                castingState = CastingState.STARTCAST;
                return;
        } 
    }

}
