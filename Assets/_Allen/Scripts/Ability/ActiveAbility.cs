using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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
    float currentCooldown = 0;

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
        if (currentCooldown > 0) return;

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

        castingState++;
    }

    public void StopCast()
    {
        instancedAbilityOutline.SetActive(false);
    }

    private void StartAbility()
    {
        instancedAbilityOutline.SetActive(true);
    }

    private void EndAbility()
    {
        onAbilityEnd?.Invoke();


    }

    protected virtual void UseAbility()
    {   
        onAbilityStart?.Invoke();  
        
        instancedAbilityOutline.SetActive(true);
        //StartCooldown();

        EndAbility();
    }

    ///
    /// TODO : | Create AbilityComponent Class To Handle Ability Coroutines |
    ///        v                                                            v

    /*
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
        onCooldownStarted?.Invoke(cooldownDuration);
        yield return new WaitForSeconds(cooldownDuration);
        onCooldown = false;
    }
    */

}
