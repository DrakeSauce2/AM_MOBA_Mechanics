using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Item/BaseItem")]
public class Item : ScriptableObject
{
    public GameObject Owner { get; private set; }

    [SerializeField] private Image itemImage;

    [SerializeField] private Stats itemStats;

    public void Init(GameObject owner)
    {
        Owner = owner;
    }

    public virtual void Update()
    {
        // Updates Aura Items and Active Items
    }

    public void ApplyStats(Stats ownerStats)
    {
        ownerStats.TryAddStatValue(itemStats);
    }

    public void RemoveStats(Stats ownerStats)
    {
        ownerStats.TryRemoveStatValue(itemStats);
    }
}
