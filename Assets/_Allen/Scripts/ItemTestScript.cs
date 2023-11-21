using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTestScript : MonoBehaviour
{
    public void GiveItem(Item itemToGive)
    {
        FindAnyObjectByType<PlayerCharacter>().inventoryComponent.TryAddItemToInventory(itemToGive);
    }
}
