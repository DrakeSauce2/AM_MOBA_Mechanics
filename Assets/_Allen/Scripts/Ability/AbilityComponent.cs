using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityComponent : MonoBehaviour
{
    [Header("Abilities")]
    [SerializeField] private PassiveAbility passiveAbility;
    [SerializeField] private List<ActiveAbility> activeAbilities = new List<ActiveAbility>(4);

    public delegate void OnAbilityAdded(ActiveAbility ability);
    public event OnAbilityAdded onAbilityAdded;

    public void Start()
    {
        for (int i = 0; i < activeAbilities.Count; i++)
        {
            onAbilityAdded?.Invoke(activeAbilities[i]);
        }
    }

    public void TryUseAbility(int index)
    {
        //activeAbilities[index].Cast();
    }


}
