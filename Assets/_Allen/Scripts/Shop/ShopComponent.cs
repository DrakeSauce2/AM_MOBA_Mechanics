using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopComponent : MonoBehaviour
{
    private PlayerCharacter player;

    bool toggleShop = false;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerCharacter>();
    }
    public void ToggleShop()
    {
        toggleShop = !toggleShop;

        Cursor.visible = toggleShop;
        gameObject.SetActive(toggleShop);
    }

    public void BuyItem(Item item)
    {
        player.inventoryComponent.TryAddItemToInventory(item);
    }



}
