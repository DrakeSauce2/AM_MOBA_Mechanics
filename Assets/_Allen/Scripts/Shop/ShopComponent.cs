using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopComponent : MonoBehaviour
{
    private PlayerCharacter player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerCharacter>();
    }

    public void BuyItem(Item item)
    {
        player.inventoryComponent.TryAddItemToInventory(item);
    }



}
