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

    private Stats stats;

    public void Init(GameObject owner, Stats owningStats)
    {
        stats = owningStats;    

        for (int i = 0; i < activeAbilities.Count; i++)
        {
            activeAbilities[i].Init(this, owner, slotList[i], owningStats);
        }
    }

    public void TryUseAbility(Animator animator, int index)
    {
        if(stats.TryGetStatValue(Stat.MANA) > 0)

        activeAbilities[index].Cast(animator, index);
    }


}
