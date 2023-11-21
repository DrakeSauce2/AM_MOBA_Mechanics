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
