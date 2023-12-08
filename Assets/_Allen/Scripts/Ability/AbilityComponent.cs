using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityComponent : MonoBehaviour
{
    [Header("Ability Slots")]
    [SerializeField] private List<AbilitySlot> slotList = new List<AbilitySlot>();

    [Header("Abilities")]
    [SerializeField] private PassiveAbility passiveAbility;
    [SerializeField] private List<ActiveAbility> activeAbilities = new List<ActiveAbility>(4);
    public List<ActiveAbility> ActiveAbilities { get { return activeAbilities; } }

    public delegate void OnAbilityAdded(ActiveAbility ability);
    public event OnAbilityAdded onAbilityAdded;

    public void Init(GameObject owner)
    {
        for (int i = 0; i < activeAbilities.Count; i++)
        {
            activeAbilities[i].Init(this, owner, slotList[i]);
        }
    }

    public void TryUseAbility(int index)
    {
        activeAbilities[index].Cast();
    }


}
