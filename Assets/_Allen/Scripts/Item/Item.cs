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
    private Stats instancedStats;
    public Stats GetItemStats() { return instancedStats; }

    [Header("Item Description")]
    [SerializeField, TextArea] private string itemDescription;
    public string ItemDescription { get { return itemDescription; } }

    public float ItemBuyCost { get { return instancedStats.TryGetStatValue(Stat.ITEMBUY_COST); } }
    public float ItemSellCost { get { return instancedStats.TryGetStatValue(Stat.ITEMSELL_COST); } }

    public void SetOwner(GameObject owner)
    {
        Owner = owner;
    }

    public void Init()
    {
        instancedStats = Instantiate(itemStats);
        instancedStats.Initialize();
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
