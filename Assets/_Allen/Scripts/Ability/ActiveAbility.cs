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

    [Header("Mana Cost")]
    [SerializeField] private float manaCost;

    [Header("Base Damage")]
    [SerializeField] private float baseDamage;

    //

    public delegate void OnAbilityStart();
    public delegate void OnAbilityEnd();
    public OnAbilityStart onAbilityStart;   
    public OnAbilityEnd onAbilityEnd;

    public Action<float> onCooldownStarted;

    //

    public bool onCooldown { get; private set; }

    private Stats stats;

    public AbilityComponent OwningAblityComponet
    {
        get;
        private set;
    }


    public void Init(AbilityComponent abilityComponent, GameObject owner, AbilitySlot abilitySlot, Stats owningStats)
    {
        OwningAblityComponet = abilityComponent;
        Owner = owner;
        stats = owningStats;

        onCooldown = false;

        slot = abilitySlot;
        slot.Init(this, abilityImageSprite);

        instancedAbilityOutline = Instantiate(abilityOutlinePrefab, Owner.transform.position,
                                              Quaternion.identity, Owner.transform);

        instancedAbilityOutline.SetActive(false);
    }

    public void Cast(Animator animator, int index)
    {
        if (onCooldown) return;

        switch (castingState)
        {
            case CastingState.STARTCAST:
                StartAbility();
                return;
            case CastingState.CAST:
                animator.SetTrigger($"Ability{index + 1}");
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
        
        instancedAbilityOutline.SetActive(false);

        GameObject damageArea = Instantiate(abilityOutlinePrefab, Owner.transform.position,
                                            Owner.transform.rotation);

        Color color = new Color(1, 1, 1, 0);
        damageArea.GetComponentInChildren<MeshRenderer>().material.color = color;

        DamageTrigger damageTrigger = damageArea.AddComponent<DamageTrigger>();
        damageTrigger.Init(Owner, baseDamage + stats.TryGetStatValue(Stat.PHYSICAL_POWER) * stats.TryGetStatValue(Stat.PHYSICAL_POWER_SCALE) + 
                                  stats.TryGetStatValue(Stat.MAGICAL_POWER) * stats.TryGetStatValue(Stat.MAGICAL_POWER_SCALE),
                                  0.5f);

        stats.TryAddStatValue(Stat.MANA, -manaCost);
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
