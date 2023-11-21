using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    private Stats ownerStats;

    private List<Item> inventorySlots = new List<Item>();

    public void Init(Stats ownerStats)
    {
        this.ownerStats = ownerStats;
    }

    public void TryAddItemToInventory(Item itemToAdd)
    {
        if (CheckItemIsInInventory(itemToAdd)) return;
        if (IsFull()) return;

        inventorySlots.Add(itemToAdd);
        ownerStats.TryAddStatValue(itemToAdd.GetItemStats());
    }

    public void RemoveItem(Item itemToRemove)
    {
        inventorySlots.Remove(itemToRemove);
        ownerStats.TryRemoveStatValue(itemToRemove.GetItemStats());
    }

    public bool CheckItemIsInInventory(Item itemToCheck)
    {
        foreach (Item item in inventorySlots)
        {
            if (item == itemToCheck)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsFull()
    {
        return inventorySlots.Count < 6;
    }

}
