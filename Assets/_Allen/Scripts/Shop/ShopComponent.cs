using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopComponent : MonoBehaviour
{
    private PlayerCharacter player;
    private Stats playerStats;

    [SerializeField] TextMeshProUGUI itemDescriptionText;

    bool toggleShop = false;

    private ShopItem selectedShopItem = null;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerCharacter>();
        playerStats = player.GetStats();
    }

    public void ToggleShop()
    {
        toggleShop = !toggleShop;

        Cursor.visible = toggleShop;
        gameObject.SetActive(toggleShop);
    }

    public void BuyItem()
    {
        if (selectedShopItem == null) return;
        if (player.inventoryComponent.CheckItemIsInInventory(selectedShopItem.item) == true) return;

        if (playerStats == null)
        {
            Debug.Log("Not Sure Why This Is Now Returning Null?");
            playerStats = player.GetStats();
        }

        if (playerStats.TryGetStatValue(Stat.GOLD) < selectedShopItem.item.ItemBuyCost) return;


        playerStats.TryAddStatValue(Stat.GOLD, -selectedShopItem.item.ItemBuyCost);
        player.inventoryComponent.TryAddItemToInventory(selectedShopItem.item);
    }

    public void SellItem()
    {
        if (selectedShopItem == null) return;
        if (player.inventoryComponent.CheckItemIsInInventory(selectedShopItem.item) == false) return;

        playerStats.TryAddStatValue(Stat.GOLD, selectedShopItem.item.ItemSellCost);
        player.inventoryComponent.RemoveItem(selectedShopItem.item);
    }

    public void SelectShopItem(ShopItem selectedShopItem)
    {
        this.selectedShopItem = selectedShopItem;
        itemDescriptionText.text = selectedShopItem.item.ItemDescription;
    }


}
