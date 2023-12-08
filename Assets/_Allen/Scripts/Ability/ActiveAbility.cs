using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public enum CastingState
{
    STARTCAST,
    CAST,
    INPROGRESS
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
    
    [Header("Cooldown")]
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

    public Action<float> onCooldownStarted;

    //

    private bool onCooldown = false;

    public AbilityComponent OwningAblityComponet
    {
        get;
        private set;
    }


    public void Init(AbilityComponent abilityComponent, GameObject owner, AbilitySlot abilitySlot)
    {
        OwningAblityComponet = abilityComponent;
        Owner = owner;
        slot = abilitySlot;
        slot.Init(this, abilityImageSprite);

        instancedAbilityOutline = Instantiate(abilityOutlinePrefab, Owner.transform.position + outlinePositionOffset,
                                              Quaternion.identity, Owner.transform);

        instancedAbilityOutline.SetActive(false);
    }

    public void Cast()
    {
        if (onCooldown) return;

        switch (castingState)
        {
            case CastingState.STARTCAST:
                StartAbility();
                return;
            case CastingState.CAST:
                
                UseAbility();
                return;
            case CastingState.INPROGRESS:

                return;
        }

        castingState++;
    }

    public void StopCast()
    {
        instancedAbilityOutline.SetActive(false);
        castingState = CastingState.STARTCAST;
    }

    private void StartAbility()
    {
        instancedAbilityOutline.SetActive(true);
        castingState = CastingState.CAST;
    }

    private void EndAbility()
    {
        onAbilityEnd?.Invoke();


    }

    protected virtual void UseAbility()
    {   
        onAbilityStart?.Invoke();  
        
        instancedAbilityOutline.SetActive(true);

        StartCooldown();

        EndAbility();
    }
    
    void StartCooldown()
    {
        StartCoroutine(CooldownCoroutine());
    }

    public Coroutine StartCoroutine(IEnumerator enumerator)
    {
        return OwningAblityComponet.StartCoroutine(enumerator);
    }

    IEnumerator CooldownCoroutine()
    {
        onCooldown = true;
        onCooldownStarted?.Invoke(cooldown);
        castingState = CastingState.INPROGRESS;

        yield return new WaitForSeconds(cooldown);

        onCooldown = false;
        onAbilityEnd?.Invoke();
        castingState = CastingState.STARTCAST;
    }
    

}
