using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Item/BaseItem")]
public class Item : ScriptableObject
{
    public GameObject Owner { get; private set; }

    [Header("Graphic")]
    [SerializeField] private Sprite itemIcon;
    public Sprite ItemIcon { get { return itemIcon; } }

    [Header("Item Stats")]
    [SerializeField] private Stats itemStats;
    public Stats GetItemStats() { return itemStats; }

    [Header("Item Description")]
    [SerializeField, TextArea] private string itemDescription;
    public string ItemDescription { get { return itemDescription; } }

    public float ItemBuyCost { get { return itemStats.TryGetStatValue(Stat.ITEMBUY_COST); } }
    public float ItemSellCost { get { return itemStats.TryGetStatValue(Stat.ITEMSELL_COST); } }

    public void SetOwner(GameObject owner)
    {
        Owner = owner;
    }

    public void Init()
    {
        itemStats.Initialize();
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
