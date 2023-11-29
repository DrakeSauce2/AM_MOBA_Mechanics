using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemTiers
{
    RANK_1,
    RANK_2,
    RANK_3
}

[CreateAssetMenu(menuName = "Item/BaseItem")]
public class Item : ScriptableObject
{
    public GameObject Owner { get; private set; }

    [Header("Item Tier")]
    [SerializeField] private ItemTiers itemTier;
    public ItemTiers ItemTier { get { return itemTier; } }

    [Header("Graphic")]
    [SerializeField] private Sprite itemIcon;
    public Sprite ItemIcon { get { return itemIcon; } }

    [Header("Item Stats")]
    [SerializeField] private Stats itemStats;
    public Stats GetItemStats() { return itemStats; }

    public void Init(GameObject owner)
    {
        Owner = owner;
    }

    public void RemoveItem()
    {
        Destroy(this);
    }

    public virtual void Update()
    {
        // Updates Aura Items and Active Items
    }

}
